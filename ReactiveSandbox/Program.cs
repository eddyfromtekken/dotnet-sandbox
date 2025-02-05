// See https://aka.ms/new-console-template for more information

using System.Reactive.Concurrency;
using System.Reactive.Linq;

public static class Program
{
    public static async Task Main()
    {
        Ticks();
    }

    public static void Ticks()
    {
        IObservable<long> ticks = Observable.Timer(
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromSeconds(100));

        ticks.Subscribe(
            tick => Console.WriteLine($"Tick {tick}"));
    }
    
    public static IObservable<IList<T>> Quiescent<T>(
        this IObservable<T> src,
        TimeSpan minimumInactivityPeriod)
    {
        IObservable<int> onoffs =
            from _ in src
            from delta in 
                Observable.Return(1)
                    .Concat(Observable.Return(-1)
                        .Delay(minimumInactivityPeriod))
            select delta;
        IObservable<int> outstanding = onoffs.Scan(0, (total, delta) => total + delta);
        IObservable<int> zeroCrossings = outstanding.Where(total => total == 0);
        return src.Buffer(zeroCrossings);
    }
}