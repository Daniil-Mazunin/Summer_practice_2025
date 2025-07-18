using Xunit;
using task18;
using task19;
using CommandLib;

public class TestCommandTest
{
    [Fact]
    public void FiveTestCommand_ExecuteThreeTimesEach_HardStop()
    {
        var scheduler = new RoundRobbinScheduler();
        var server = new ServerThread(scheduler);

        var commands = new TestCommand[5];
        for (int i = 0; i < 5; i++)
        {
            commands[i] = new TestCommand(scheduler, i + 1, 3);
            scheduler.Add(commands[i]);
        }

        Thread.Sleep(1000);
        
        server.EnqueueCommand(new HardStop(server));

        bool finished = server._thread.Join(3000);
        Assert.True(finished);

        foreach (var command in commands)
        {
            Assert.Equal(3, command.Counter);
        }
    }
}
