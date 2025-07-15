using task14;
using System.Diagnostics;
using System.Text.Json;

namespace task15;

class Program
{
    static void Main()
    {
        double a = -100;
        double b = 100;
        Func<double, double> function = Math.Sin;
        double targetAccuracy = 1e-4;
        double[] steps = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
        double optimalStep = 0;

        foreach (var step in steps)
        {
            double result = DefiniteIntegral.TrapezoidMethod(a, b, function, step);
            if (Math.Abs(result) <= targetAccuracy)
            {
                optimalStep = step;
                break;
            }
        }

        int[] threadsCount = { 1, 2, 4, 6, 8, 10 };
        int iterations = 10;
        double[] times = new double[threadsCount.Length];

        for (int i = 0; i < threadsCount.Length; i++)
        {
            int threads = threadsCount[i];
            double totalTime = 0;

            for (int j = 0; j < iterations; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                double result = DefiniteIntegral.Solve(a, b, function, optimalStep, threads);
                stopwatch.Stop();
                totalTime += stopwatch.Elapsed.TotalMilliseconds;
            }

            times[i] = totalTime / iterations;
        }

        double singleThreadTime = 0;
        for (int i = 0; i < iterations; i++)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            double result = DefiniteIntegral.TrapezoidMethod(a, b, function, optimalStep);
            stopwatch.Stop();
            singleThreadTime += stopwatch.Elapsed.TotalMilliseconds;
        }

        singleThreadTime /= iterations;

        double multiThreadTime = times[0];
        int optimalThreads = threadsCount[0];
        for (int i = 1; i < times.Length; i++)
        {
            if (times[i] < multiThreadTime)
            {
                multiThreadTime = times[i];
                optimalThreads = threadsCount[i];
            }
        }

        double diffTime = (singleThreadTime - multiThreadTime) / singleThreadTime * 100;

        string txtPath = "./result.txt";

        File.WriteAllText(txtPath,
            $"Оптимальный шаг: {optimalStep}\n" +
            $"Оптимальное количество потоков: {optimalThreads}\n" +
            $"Время работы многопоточной версии: {multiThreadTime:F4} мс\n" +
            $"Время работы однопоточной версии: {singleThreadTime:F4} мс\n" +
            $"Разница во времени: {diffTime:F2}%\n");

        var chartData = new
        {
            ThreadsCount = threadsCount,
            Times = times
        };
        string json = JsonSerializer.Serialize(chartData);
        File.WriteAllText("chartData.json", json);
    }
}
