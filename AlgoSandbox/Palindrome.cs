using System.Globalization;

namespace AlgoSandbox;

public static class Palindrome
{
    public static string Get(int index, int length)
    {
        var l = (length + 1) / 2;
        var numberStr = (Math.Pow(10, l) + index).ToString(CultureInfo.InvariantCulture);
        return $"{numberStr}{Reverse(numberStr)}";
    }


    public static string Reverse(string str)
    {
        Span<char> span = stackalloc char[str.Length];
        str.AsSpan().CopyTo(span);
        span.Reverse();
        return new string(span);
    }
}