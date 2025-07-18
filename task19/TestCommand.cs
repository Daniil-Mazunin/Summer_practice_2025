﻿using task18;
using CommandLib;
using System.Diagnostics;

namespace task19;

public class TestCommand : ICommand
{
    public int Counter = 0;
    private int Id;
    private int MaxCount;
    private IScheduler Scheduler;
    private Stopwatch stopwatch = new Stopwatch();
    public long ElapsedMs => stopwatch.ElapsedMilliseconds;
    public TestCommand(IScheduler scheduler, int id, int maxCount)
    {
        Scheduler = scheduler;
        Id = id;
        MaxCount = maxCount;
    }
    public void Execute()
    {
        if (Counter == 0)
        {
            stopwatch.Start();
        }
        if (Counter < MaxCount)
        {
            Console.WriteLine($"Поток {Id} вызов {++Counter}");
            Scheduler.Add(this);
        }
        else
        {
            stopwatch.Stop();
        }
    }
}
