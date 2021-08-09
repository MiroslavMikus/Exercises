using System;
using System.Linq;
using System.Reflection;

namespace HalloConfig.Menu
{
    public static class ReflectionTools
    {
        public static MethodInfo[] GetCommands(Type assemblyType)
        {
            return assemblyType.Assembly.GetTypes()
                .Where(a => a.IsAbstract && a.IsSealed)
                .SelectMany(a => a.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Where(a => a.Name.EndsWith("Command"))
                .ToArray();
        }
    }
}