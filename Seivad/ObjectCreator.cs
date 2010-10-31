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

        public Object GetInstance()
        {
            return GetInstance(null);
        }

        public Object GetInstance(IArguments args)
        {
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
            var lengthMatches = constructors.Where(c => c.GetParameters().Count() == args.GetArguments().Count());

            var matches = GetMatchingConstructors(lengthMatches, args);

            if (matches.Count == 0) {
                throw new NotSupportedException("Unable to find matching constructor");
            }

            var result = Activator.CreateInstance(ReturnType, args.GetArguments().Select(a => a.Value).ToArray());

            if (_onCreation != null)
            {
                _onCreation(result);
            }

            return result;
        }

        private List<ConstructorInfo> GetMatchingConstructors(IEnumerable<ConstructorInfo> constructors, IArguments args)
        { 
            var matches = new List<ConstructorInfo>();

            foreach (var item in constructors)
            {
                var parameters = item.GetParameters().Select(p => p.Name);

                if (args.GetArguments().All(a => parameters.Contains(a.Key)))
                {
                    matches.Add(item);
                }
            }


            return matches;
        }
    }
}
