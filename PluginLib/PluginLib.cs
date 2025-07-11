namespace PluginLib;

public interface ICommand
{
    void Execute();
}

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoad : Attribute
{
    public string[] Dependencies { get; }
    public PluginLoad(params string[] dependencies)
    {
        Dependencies = dependencies;
    }
}
