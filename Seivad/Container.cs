using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    public class Container
    {

        private readonly IDictionary<Type, ObjectGraph> _registry;

        public Container() {
            _registry = new Dictionary<Type, ObjectGraph>();
        }

        public ObjectSetup<T> Register<T>()
        {
            var type = typeof(T);

            if (_registry.ContainsKey(type))
                throw new ArgumentException(string.Format("The type {0} has already been registered.", type.Name));

            var graph = new ObjectGraph();

            _registry.Add(type, graph);

            return new ObjectSetup<T>(graph);
        }

        public T GetIntance<T>() {

            var type = typeof(T);

            if (!_registry.ContainsKey(type))
                throw new ArgumentException(string.Format("The type {0} has not been registered", type.Name));

            return _registry[type].GetIntance<T>();
        }

    }

}
