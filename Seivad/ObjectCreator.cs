using System;
using System.Linq;
using Seivad.Args;

namespace Seivad
{
    internal class ObjectCreator
    {
        public Type ReturnType { get; set; }
        public bool IsSingleton { get; set; }

        private Action<Object> _onCreation;
        private readonly IRegistry _registry;

        static readonly Object Padlock = new Object();
        static Object _instance;

        internal ObjectCreator(IRegistry registry)
        {
            ReturnType = null;
            IsSingleton = false;

            _onCreation = null;
            _registry = registry;
        }

        public void SetOnCreation(Action<Object> action)
        {
            _onCreation = action;
        }

        internal Object GetInstance(Arguments args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (IsSingleton == false)
            {
                return CreateInstanceAndApplyAction(args);
            }

            lock (Padlock)
            {
                if (_instance == null)
                {
                    _instance = CreateInstanceAndApplyAction(args);
                }
            }

            return _instance;

        }

        private object CreateInstanceAndApplyAction(Arguments args)
        {
            var constructors = ReturnType.GetConstructors();
           
            var selector = new ConstructorSelector.Selector(_registry);
            var constructorData = selector.GetConstructorData(constructors, args);

            object result = constructorData.Constructor.Invoke(constructorData.Arguments.Select(a => a.Value).ToArray());

            if (_onCreation != null)
            {
                _onCreation(result);
            }

            return result;
        }

    }
}
