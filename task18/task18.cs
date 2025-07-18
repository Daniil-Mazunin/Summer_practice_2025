using CommandLib;
using System.Collections.Concurrent;
using System.Threading;

namespace task18;

public class ServerThread
{
    public Thread _thread;
    private BlockingCollection<ICommand> CommandQueue = new BlockingCollection<ICommand>();
    public bool IsRunning = true;
    private bool SoftStopRequested = false;
    private IScheduler _scheduler;
    public ServerThread(IScheduler scheduler)
    {
        _scheduler = scheduler;
        _thread = new Thread(Run);
        _thread.Start();
    }
    private void Run()
    {
        while (IsRunning)
        {
            if (CommandQueue.TryTake(out ICommand command, 100)) 
            {
                _scheduler.Add(command);
            }
            if (_scheduler.HasCommand())
            {
                var cmd = _scheduler.Select();
                if (cmd != null)
                {
                    cmd.Execute();
                    continue;
                }
            }
            if (SoftStopRequested && !_scheduler.HasCommand())
            {
                IsRunning = false;
            }
        }
    }
    public void EnqueueCommand(ICommand command)
    {
        if (IsRunning)
        {
            CommandQueue.Add(command);
        }
    }
    public void HardStop()
    {
        IsRunning = false;
    }
    public void SoftStop()
    {
        SoftStopRequested = true;
        if (!_scheduler.HasCommand())
        {
            IsRunning = false;
        }
    }
}
public class HardStop : ICommand
{
    private ServerThread _serverThread;
    public HardStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }
    public void Execute()
    {
        _serverThread.HardStop();
    }
}
public class SoftStop : ICommand
{
    private ServerThread _serverThread;
    public SoftStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }
    public void Execute()
    {
        _serverThread.SoftStop();
    }
}
public class LongRunningCommand : ICommand
{
    private IScheduler _scheduler;
    public int TotalSteps;
    public int CurrentStep = 0;
    public LongRunningCommand(IScheduler scheduler, int totalSteps)
    {
        _scheduler = scheduler;
        TotalSteps = totalSteps;
    }
    public void Execute()
    {
        if (CurrentStep < TotalSteps)
        {
            CurrentStep++;
            if (CurrentStep < TotalSteps)
            {
                _scheduler.Add(this);
            }
        }
    }
}
