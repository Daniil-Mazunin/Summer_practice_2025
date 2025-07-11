using PluginLib;

namespace Plugin3;

[PluginLoad(["Plugin2", "Plugin1"])]
public class Plugin3 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Plugin3");
    }
}
