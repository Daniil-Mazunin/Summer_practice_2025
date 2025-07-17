using task18;
using CommandLib;

namespace task19;

public class TestCommand : ICommand
{
    public int Counter = 0;
    private int Id;
    private int MaxCount;
    private IScheduler Scheduler;
    public TestCommand(IScheduler scheduler, int id, int maxCount)
    {
        Scheduler = scheduler;
        Id = id;
        MaxCount = maxCount;
    }
    public void Execute()
    {
        if (Counter < MaxCount)
        {
            Console.WriteLine($"Поток {Id} вызов {++Counter}");
            if (Counter < MaxCount)
            {
                Scheduler.Add(this);
            }
        }
    }
}
