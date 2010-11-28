using System;
using System.Linq;
using System.Reflection;
using Seivad.Args;

namespace Seivad.ConstructorSelector
{
    internal static class Extensions
    {
        public static bool Contains(this ParameterInfo[] parameters, Argument arg)
        {
            Type argType = arg.Value.GetType();
            return parameters.Any(p => p.Name == arg.Name && p.ParameterType == argType);
        }
    }
}