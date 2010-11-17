using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Seivad
{
    internal class ObjectCreator
    {
        public Type ReturnType { get; set; }
        public bool IsSingleton { get; set; }

        private Action<Object> _onCreation;

        static readonly Object _padlock = new Object();
        static Object _instance;

        internal ObjectCreator()
        {
            ReturnType = null;
            IsSingleton = false;

            _onCreation = null;
        }

        public void SetOnCreation(Action<Object> action)
        {
            _onCreation = action;
        }

        internal Object GetInstance(IArguments args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (IsSingleton == false)
            {
                return CreateInstanceAndApplyAction(args);
            }

            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = CreateInstanceAndApplyAction(args);
                }
            }

            return _instance;

        }

        private object CreateInstanceAndApplyAction(IArguments args)
        {
            var constructors = ReturnType.GetConstructors();
            var matches = ConstructorsWithAllArguments(constructors, args);

            if (matches.Count == 0)
            {
                throw new ConstructorException("No matching public constructors found");
            }

            var result = Activator.CreateInstance(ReturnType, args.ToDictionary().Select(a => a.Value).ToArray());

            if (_onCreation != null)
            {
                _onCreation(result);
            }

            return result;
        }

        private List<ConstructorInfo> ConstructorsWithAllArguments(IEnumerable<ConstructorInfo> constructors, IArguments args)
        {
            //get constructors that contain all of the args
            return constructors.Where(c => args.ToDictionary().Select(d => d.Key).All(name => c.GetParameters().Select(p => p.Name).Contains(name)))
                               .OrderBy(c => c.GetParameters().Count())
                               .ToList();
        }


    }
}
