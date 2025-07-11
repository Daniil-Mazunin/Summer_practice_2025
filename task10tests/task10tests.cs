using Xunit;
using task10;
using Plugin1;
using Plugin2;
using Plugin3;

public class PluginLoaderTest
{
    [Fact]
    public void PluginLoader_LoadsPluginsBasedOnDependencies()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        string currentDirectory = Directory.GetCurrentDirectory();
        string pluginsDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", "dllFiles"));

        var pluginLoader = new PluginLoader();
        string expendOutput = "Plugin2 -> Plugin1 -> Plugin3";

        pluginLoader.Loader(pluginsDirectory);

        Assert.Contains(expendOutput, output.ToString());
    }

    [Fact]
    public void PluginLoader_DirectoryNotExist()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        string currentDirectory = Directory.GetCurrentDirectory();
        string notExistDir = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", "NotExistDir"));

        var pluginLoader = new PluginLoader();
        
        Assert.Throws<DirectoryNotFoundException>(() => pluginLoader.Loader(notExistDir));
    }
}  
