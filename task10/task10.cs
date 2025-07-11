using PluginLib;
using System.Reflection;

namespace task10;

public class PluginLoader
{
    public void Loader(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new DirectoryNotFoundException();
        }
        var dllFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
        var assemblies = new List<Assembly>();
        foreach (var dllFile in dllFiles)
        {
            Assembly assembly = Assembly.LoadFrom(dllFile);
            assemblies.Add(assembly);
        }
        var allPlugins = new List<Type>();
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<PluginLoad>() != null)
                {
                    allPlugins.Add(type);
                }
            }
        }
        var independentPlugins = new List<Type>();
        foreach (var plugin in allPlugins)
        {
            var attribute = plugin.GetCustomAttribute<PluginLoad>();
            if (attribute!.Dependencies.Length == 0)
            {
                independentPlugins.Add(plugin);
            }
        }
        var plugins = new List<Type>();
        while (independentPlugins.Any())
        {
            var independentPlugin = independentPlugins[0];
            plugins.Add(independentPlugin);
            independentPlugins.RemoveAt(0);
            foreach (var plugin in allPlugins.Except(plugins))
            {
                var dependencies = plugin.GetCustomAttribute<PluginLoad>()!.Dependencies.Except(plugins.Select(d => d.Name));
                if (!dependencies.Any())
                {
                    independentPlugins.Add(plugin);
                }
            }
        }
        foreach (var p in plugins)
        {
            var plugin = Activator.CreateInstance(p);
            var method = p.GetMethod("Execute");
            method!.Invoke(plugin, null);
        }
    }
}
