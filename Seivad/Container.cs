using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Seivad.Args;

namespace Seivad
{
    public class Container
    {

        private readonly IDictionary<Type, ObjectCreator> _registry;

        public Container()
        {
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
                throw new RegistrationException(string.Format("The type {0} has already been registered.", type.Name));

            return new ObjectSetup<TRequest>(this);
        }

        public T GetInstance<T>()
        {
            return GetInstance<T>(null);
        }

        public T GetInstance<T>(Arguments args)
        {

            var type = typeof(T);

            if (!_registry.ContainsKey(type))
                throw new RegistrationException(string.Format("The type {0} has not been registered", type.Name));

            return (T)_registry[type].GetInstance(args);
        }

    }

}
