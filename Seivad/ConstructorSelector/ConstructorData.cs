using System.Reflection;
using Seivad.Args;

namespace Seivad.ConstructorSelector
{
    internal class ConstructorData
    {
        public ConstructorInfo Constructor { get; private set; }
        public Arguments Arguments { get; private set; }

        internal ConstructorData(ConstructorInfo constructor)
        {
            Constructor = constructor;
            Arguments = new Arguments();
        }
        
        internal ConstructorData(ConstructorInfo constructor, Arguments args)
        {
            Constructor = constructor;
            Arguments = args;
        }
    }
}
