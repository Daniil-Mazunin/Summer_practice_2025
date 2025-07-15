using System.Threading;

namespace task14;

public class DefiniteIntegral
{
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double[] results = new double[threadsnumber];

        double lenght = (b - a) / threadsnumber;

        Parallel.For(0, threadsnumber, i =>
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

            results[i] = TrapezoidMethod(start, end, function, step);
        });

        return results.Sum();
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
