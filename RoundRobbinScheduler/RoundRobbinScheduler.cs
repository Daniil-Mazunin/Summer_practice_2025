using CommandLib;

namespace task18;

public class RoundRobbinScheduler : IScheduler
{
    private Queue<ICommand> commands = new Queue<ICommand>();
    public bool HasCommand()
    {
        return commands.Count > 0;
    }
    public ICommand Select()
    {
        if (commands.Count > 0)
        {
            var command = commands.Dequeue();
            commands.Enqueue(command); 
            return command;
        }
        return null;
    }
    public void Add(ICommand cmd)
    {
        if (cmd != null)
        {
            commands.Enqueue(cmd);
        }
    }
}
