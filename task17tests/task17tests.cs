using Xunit;
using task17;
using CommandLib;

public class TestCommand : ICommand
{
    public void Execute()
    {
        Thread.Sleep(100);
    }
}
public class ServerThreadTests
{
    [Fact]
    public void HardStop_ShouldStopsThreadImmediately()
    {
        var server = new ServerThread();

        server.EnqueueCommand(new TestCommand());
        server.EnqueueCommand(new HardStop(server));
        server.EnqueueCommand(new TestCommand());

        Thread.Sleep(300);

        Assert.False(server.IsRunning);
    }

    [Fact]
    public void HardStop_ReturnsErrorWhenExecutedInWrongThread()
    {
        var server = new ServerThread();
        var hardStop = new HardStop(server);

        Assert.Throws<InvalidOperationException>(() => hardStop.Execute());
    }

    [Fact]
    public void SoftStop_StopsThreadAfterAllCommands()
    {
        var server = new ServerThread();

        server.EnqueueCommand(new TestCommand());
        server.EnqueueCommand(new SoftStop(server));
        server.EnqueueCommand(new TestCommand());

        Thread.Sleep(500);

        Assert.False(server.IsRunning);
    }

    [Fact]
    public void SoftStop_ReturnsErrorWhenExecutedInWrongThread()
    {
        var server = new ServerThread();
        var softStop = new SoftStop(server);

        Assert.Throws<InvalidOperationException>(() => softStop.Execute());
    }
}
