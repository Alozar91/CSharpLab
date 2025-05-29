using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._4_ServerMT
{
    internal class Program
    {
        const int MAX_CONNECTION_IN_QUEUE = 10;
        const int CLIENTS = 10;

        static void Main(string[] args)
        {
            int req = 0;
            object lockObject = new object();
            ThreadPool.SetMaxThreads(CLIENTS, CLIENTS);

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 1111);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(MAX_CONNECTION_IN_QUEUE);

            while (req < 1000)
            {
                Socket client = socket.Accept();
                try
                {
                    _ = Task.Run(() =>
                    {
                        Console.WriteLine($"Remote client: {client.RemoteEndPoint} ");
                        using var stream = new NetworkStream(client);
                        using var r = new StreamReader(stream, Encoding.UTF8);
                        using var w = new StreamWriter(stream, Encoding.UTF8);
                        string result = r.ReadLine();
                        lock (lockObject) req++;
                        Console.WriteLine($"Received: {result}, Requests: {req}");
                        Thread.Sleep(100); //

                        w.WriteLine(result.ToUpper());
                        w.Flush();
                        client.Dispose();
                    });
                }
                catch (AggregateException ex)
                {
                    foreach (var inner in ex.InnerExceptions)
                        if (inner is TaskCanceledException)
                            Console.WriteLine("The task canceled");
                        else
                            Console.WriteLine(inner.Message);
                }
            }
            Task.WaitAll();
        }
    }
}
