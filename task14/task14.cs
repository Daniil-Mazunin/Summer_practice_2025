using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double result = 0.0;
        double lenght = (b - a) / threadsnumber;

        var barrier = new Barrier(threadsnumber + 1);
        object locker = new object();

        for (int i = 0; i < threadsnumber; i++)
        {
            double start = a + i * lenght;
            double end;
            if (i == threadsnumber - 1)
            {
                end = b;
            }
            else
            {
                end = start + lenght;
            }
            var thread = new Thread(() =>
            {
                lock (locker)
                {
                    result += TrapezoidMethod(start, end, function, step);
                }
                barrier.SignalAndWait();
            });
            thread.Start();
        }
        barrier.SignalAndWait();

        return result;
    }
    public static double TrapezoidMethod(double a, double b, Func<double, double> function, double step)
    {
        double result = 0.0;
        double x = a;
        while (x < b)
        {
            double nextX = Math.Min(x + step, b);
            result += (function(x) + function(nextX)) * (nextX - x) * 0.5;
            x = nextX;
        }
        return result;
    }
}
