using Xunit;
using task18;
using CommandLib;

public class TestCommand : ICommand
{
    public int ExecuteCount { get; private set; } = 0;
    public bool IsCompleted { get; private set; } = false;

    public void Execute()
    {
        if (!IsCompleted)
        {
            ExecuteCount++;
            if (ExecuteCount >= 4)
            {
                IsCompleted = true;
            }
        }
    }
}
public class SchedulerTests
{
    [Fact]
    public void ExecuteSimpleCommand()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);
        var command = new TestCommand();

        server.EnqueueCommand(command);

        Thread.Sleep(100);

        server.HardStop();
        server._thread.Join();

        Assert.True(command.ExecuteCount > 0);
    }

    [Fact]
    public void TestCommand_CompletesAfter4Executions()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);
        var command = new TestCommand();

        server.EnqueueCommand(command);

        Thread.Sleep(300);

        server.HardStop();
        server._thread.Join();

        Assert.True(command.IsCompleted);
        Assert.Equal(4, command.ExecuteCount);
    }

    [Fact]
    public void Scheduler_ExecutesMultipleCommands()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);
        var command1 = new TestCommand();
        var command2 = new TestCommand();

        server.EnqueueCommand(command1);
        server.EnqueueCommand(command2);

        Thread.Sleep(700);

        server.HardStop();
        server._thread.Join();

        Assert.True(command1.IsCompleted);
        Assert.True(command2.IsCompleted);
        Assert.Equal(4, command1.ExecuteCount);
        Assert.Equal(4, command2.ExecuteCount);
    }

    [Fact]
    public void LongRunningCommand_CompletesAllSteps()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);
        var longCmd = new LongRunningCommand(scheduler, 3);

        server.EnqueueCommand(longCmd);
        Thread.Sleep(300);

        Assert.Equal(3, longCmd.CurrentStep);

        server.HardStop();
        server._thread.Join();
    }

    [Fact]
    public void MixedCommands_ExecuteProperly()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);
        var command = new TestCommand();
        var longCommand = new LongRunningCommand(scheduler, 3);

        server.EnqueueCommand(command);
        server.EnqueueCommand(longCommand);

        Thread.Sleep(600);

        server.HardStop();
        server._thread.Join();

        Assert.True(command.IsCompleted);
        Assert.Equal(3, longCommand.CurrentStep);
    }

    [Fact]
    public void AddNullCommand_DoesNotFail()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);

        server.EnqueueCommand(null);

        Thread.Sleep(100);
        server.HardStop();
        server._thread.Join();

        Assert.True(true); 
    }
}
