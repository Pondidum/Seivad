using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    public class ObjectSetup<TRequest>
    {

        private readonly ObjectCreator _creator;
        private readonly Registry _registry;

        internal ObjectSetup(Registry registry)
        {
            _registry = registry;
            _creator = new ObjectCreator(registry);
            _registry.Add(typeof(TRequest ), _creator);
        }

        internal ObjectSetup(ObjectCreator creator)
        {
            _creator = creator;
        }

        public ObjectSetup<TRequest> Returns<TReturn>()
        {
            _creator.ReturnType = typeof(TReturn);
            return this;
        }

        public ObjectSetup<TRequest> OnCreation(Action<TRequest> action)
        {
            _creator.SetOnCreation(obj => action.Invoke((TRequest)obj));
            return this;
        }

        public ObjectSetup<TRequest> IsSingleton()
        {
            _creator.IsSingleton = true;
            return this;
        }
    }
}
