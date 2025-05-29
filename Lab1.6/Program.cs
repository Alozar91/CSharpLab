using System.Diagnostics;

namespace Lab1._6
{
    internal class Program
    {
        const int STEPS = 100_000_000;
        const int SECTIONS = 10;
        static double Single(Func<double, double> f, double a, double b, int steps = STEPS)
        {
            double w = (b - a) / steps;
            double summa = 0d;
            for (int i = 0; i < steps; i++)
            {
                double x = a + i * w + w / 2;
                double h = f(x);
                summa += h * w;
            }
            return summa;
        }
        static double SingleParallel(Func<double, double> f, double a, double b)
        {
            //double[] sectionResult = new double[SECTIONS];
            double sectionSize = (b-a) / SECTIONS;
            int sectionSteps = STEPS / SECTIONS;
            double summa = 0;
            object s = new object();
            Parallel.For(0, SECTIONS,
                (int i) =>
                {
                    double result = Single(f,
                                            sectionSize * i,
                                            sectionSize * (i + 1),
                                            sectionSteps);
                    lock (s) summa += result;
                });
            //return sectionResult.Sum();
            return summa;
        }
        static void Main(string[] args)
        {
            Stopwatch t1 = new Stopwatch();
            t1.Start();
            double r1 = Single(Math.Sin, 0, Math.PI / 2);
            t1.Stop();

            Console.WriteLine($"Single result : {r1} Time: {t1.ElapsedMilliseconds}");

            Stopwatch t2 = new Stopwatch();
            t2.Start();
            double r2 = SingleParallel(Math.Sin, 0, Math.PI / 2);
            t2.Stop();

            Console.WriteLine($"Parallel result : {r2} Time: {t2.ElapsedMilliseconds}");
        }
    }
}
