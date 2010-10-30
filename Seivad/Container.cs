using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    public class Container
    {

        private readonly IDictionary<Type, ObjectCreator> _registry;

        public Container() {
            _registry = new Dictionary<Type, ObjectCreator>();
        }

        internal void AddRegistration(Type requestedType, ObjectCreator creator)
        {
            _registry.Add(requestedType, creator);
        }

        public ObjectSetup<TRequest> Register<TRequest>()
        {
            var type = typeof(TRequest);

            if (_registry.ContainsKey(type))
                throw new ArgumentException(string.Format("The type {0} has already been registered.", type.Name));
            
            return new ObjectSetup<TRequest>(this);
        }

        public T GetIntance<T>() {

            var type = typeof(T);

            if (!_registry.ContainsKey(type))
                throw new ArgumentException(string.Format("The type {0} has not been registered", type.Name));

            return (T)_registry[type].GetInstance();
        }

    }

}
