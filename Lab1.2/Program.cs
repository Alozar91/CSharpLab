namespace Lab1._2
{
    public class MyThread
    {
        public required int Start { get; init; }
        public required int End { get; init; }
        public required string Name { get; init; }
        public Thread ParentThread { get; set; }
        public void Print()
        {
            if (ParentThread != null) {
                while (ParentThread.ThreadState== ThreadState.Unstarted) Thread.Yield();
                ParentThread.Join();
            }
            Thread.CurrentThread.Name = Name;
            for (int i = Start; i <= End; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.Name} - {i}");
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            var t0 = new Thread(new MyThread() { Start = 1, End = 100, Name = "A" }.Print);
            var t1 = new Thread(new MyThread() { Start = 1, End = 100, Name = "B", ParentThread = t0 }.Print);
            t1.Start();
            Thread.Sleep(1000);
            t0.Start();
        }
    }

}
