namespace Lab1._3
{
    class Sync
    {
        public double x = 1;
        public bool isCos = true;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Sync s = new Sync();
            Thread tCos = new Thread(() =>
            {
                for (int i = 0; i < 10; i++) {
                    lock (s)
                    {
                        while (!s.isCos) Monitor.Wait(s);
                        s.x = Math.Cos(s.x);
                        s.isCos = !s.isCos;
                        Console.WriteLine($"Thread {Thread.CurrentThread.Name} - {s.x}");
                        Monitor.Pulse(s);
                        Monitor.Wait(s);
                    }
                }
            })
            { Name = "tCos" };
            Thread tACos = new Thread(() =>
            {
                for (int i = 0; i < 10; i++) {
                    lock (s)
                    {
                        while (s.isCos) Monitor.Wait(s);
                        s.x = Math.Acos(s.x);
                        s.isCos = !s.isCos;
                        Console.WriteLine($"Thread {Thread.CurrentThread.Name} - {s.x}");
                        Monitor.Pulse(s);
                    }
                }
            })
            { Name = "tACos" };

            tCos.Start();
            tACos.Start();
        }
    }
}
