using PluginLib;

namespace Plugin2;

[PluginLoad()]
public class Plugin2 : ICommand
{
    public void Execute()
    {
        Console.Write("Plugin2 -> ");
    }
}
