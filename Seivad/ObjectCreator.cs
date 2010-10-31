using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var result = Activator.CreateInstance(ReturnType, args.GetArguments().Select(a => a.Value).ToArray());

            if (_onCreation != null)
            {
                _onCreation(result);
            }

            return result;
        }
    }
}
