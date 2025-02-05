using System.Buffers;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program
{
    public static async Task Main()
    {
        var pipe = new Pipe();
        var writingTask = WriteToPipeAsync(pipe.Writer);
        var readingTask = ReadPipeAsync(pipe.Reader);
        await Task.WhenAll(writingTask, readingTask);
    }

    private static async Task ReadPipeAsync(PipeReader reader)
    {
        while (true)
        {
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;

            foreach (var segment in buffer)
            {
                Console.WriteLine(Encoding.UTF8.GetString(segment.Span));
            }

            reader.AdvanceTo(buffer.End);
        }
    }

    private static async Task WriteToPipeAsync(PipeWriter writer)
    {
        int bufferSize = 1000;

        var listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111));
        Console.WriteLine($"Server listening on port {11111}...");

        listener.Listen(10);

        var buffer = new byte[bufferSize];


        while (true)
        {
            Console.WriteLine("Waiting for a connection...");

            Socket handler = await listener.AcceptAsync();
            Console.WriteLine("Client connected!");

            while (true)
            {
                var memory = writer.GetMemory(bufferSize);
                int bytesRead = await handler.ReceiveAsync(memory, SocketFlags.None);
                writer.Advance(bytesRead);
                await writer.FlushAsync();
            }
        }
    }
}