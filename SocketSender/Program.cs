using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program
{
    public static async Task Main()
    {
        var sender = new Socket(SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint destination = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111);


        while (true)
        {
            try
            {
                await sender.ConnectAsync(destination);
                while (true)
                {
                    await Task.Delay(10);

                    var message = "Hi friends 👋!<|EOM|>";
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    _ = await sender.SendAsync(messageBytes, SocketFlags.None);

                    Console.WriteLine($"Sent: {message}");
                }
            }
            catch(Exception)
            {
                await Task.Delay(1000);
            }
        }
    }
}