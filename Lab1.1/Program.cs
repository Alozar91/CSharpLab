using System.Threading;
namespace Lab1._1
{
    public class MyThread
    {
        public required int Start { get; init; }
        public required int End { get; init; }
        public void Print()
        {
            for (int i = Start; i <= End; i++){
                Console.WriteLine($"Thread {Thread.CurrentThread.Name} - {i}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Thread(new MyThread() { Start = 1, End = 100 }.Print) { Name="A"};
            var t2 = new Thread(new MyThread() { Start = 101, End = 200 }.Print) { Name = "B" };

            t1.Start();
            t2.Start();
        }
    }
}
