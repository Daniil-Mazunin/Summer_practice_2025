using CommandLib;
using System.Collections.Concurrent;
using System.Threading;

namespace task17;

public class ServerThread
{
    private Thread _thread;
    private BlockingCollection<ICommand> CommandQueue = new BlockingCollection<ICommand>();
    public bool IsRunning = true;
    private bool SoftStopRequested = false;
    public ServerThread()
    {
        _thread = new Thread(Run);
        _thread.Start();
    }
    private void Run()
    {
        while (IsRunning)
        {
            if (CommandQueue.TryTake(out ICommand command, Timeout.Infinite))
            {
                command.Execute();
            }
            if (SoftStopRequested && CommandQueue.Count == 0)
            {
                IsRunning = false;
            }
        }
        CommandQueue.Dispose();
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
        if (Thread.CurrentThread != _thread)
        {
            throw new InvalidOperationException("Команда HardStop может быть вызвана только из текущего потока");
        }
        IsRunning = false;
    }
    public void SoftStop()
    {
        if (Thread.CurrentThread != _thread)
        {
            throw new InvalidOperationException("Команда SoftStop может быть вызвана только из текущего потока");
        }
        SoftStopRequested = true;
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
