using PluginLib;

﻿namespace Plugin1;


[PluginLoad(["Plugin2"])]
public class Plugin1 : ICommand
{
    public void Execute()
    {
        Console.Write("Plugin1 -> ");
    }
}
