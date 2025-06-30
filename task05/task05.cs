using System;
using System.Reflection;
using System.Collections.Generic;

namespace task05;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
        => _type.GetMethods().Where(method => method.IsPublic).Select(method => method.Name);

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var method = _type.GetMethod(methodname);
        if (method != null)
        {
            return method.GetParameters().Select(name => name.Name);
        }
        else
        {
            return new List<string>();
        }
    }

    public IEnumerable<string> GetAllFields()
        => _type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Select(field => field.Name);

    public IEnumerable<string> GetProperties()
        => _type.GetProperties().Select(property => property.Name);

    public bool HasAttribute<T>() where T : Attribute
        => _type.GetCustomAttributes<T>().Any();
}
