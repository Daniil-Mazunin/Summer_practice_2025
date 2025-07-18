using task18;

namespace task19;

public class CommandGraph
{
    static void Main()
    {
        int commandCount = 5;
        int callCommand = 3;

        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);

        var commands = new List<TestCommand>();

        for (int i = 1; i <= commandCount; i++)
        {
            var command = new TestCommand(scheduler, i, callCommand);
            scheduler.Add(command);
            commands.Add(command);
        }

        while (commands.Any(c => c.Counter < callCommand))
        {
            Thread.Sleep(10); 
        }

        server.EnqueueCommand(new HardStop(server));
        server._thread.Join(); 

        double[] times = commands.Select(cmd => (double)cmd.ElapsedMs).ToArray();
        int[] commandNumbers = { 1, 2, 3, 4, 5 };

        var plot = new ScottPlot.Plot();
        plot.Add.Scatter(times, commandNumbers);
        plot.Title("Выполнение TestCommand");
        plot.XLabel("Время (мс)");
        plot.YLabel("Количество команд");
        plot.SavePng("./result.png", 600, 400);

        double totalTime = times.Sum();
        double averageTime = times.Average();

        string report =
            $"Отчет:\n" +
            $"Количество команд: {commandCount}\n" +
            $"Вызовов каждой команды: {callCommand}\n" +
            $"Минимальное время выполнения: {times.Min()} мс\n" +
            $"Максимальное время выполнения: {times.Max()} мс\n" +
            $"Среднее время на команду: {averageTime:F2} мс\n" +
            $"Время выполнения всех команд: {totalTime} мс\n";

        File.WriteAllText("./result.txt", report);
    }
}
