using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Seivad.Args;

namespace Seivad
{
    public class Container
    {

        private readonly Registry  _registry;

        public Container()
        {
            _registry = new Registry();
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

            return new ObjectSetup<TRequest>(_registry);
        }

        public T GetInstance<T>()
        {
            return GetInstance<T>(null);
        }

        public T GetInstance<T>(Arguments args)
        {
          return  _registry.Get<T>(args);
        }

    }

}
