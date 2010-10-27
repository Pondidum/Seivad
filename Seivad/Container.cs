using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    class Container
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

    class ObjectGraph
    {

        private Func<Object> _create;
        private readonly List<Action<Object>> _modifiers;

        public ObjectGraph()
        {
            _create = null;
            _modifiers = new List<Action<object>>();
        }

        internal void SetCreation(Func<Object> action)
        {
            _create = action;
        }

        internal void AddModifier(Action<object> modifier)
        {
            _modifiers.Add(modifier);
        }
        
        internal T GetIntance<T>()
        {
            var obj = _create();

            _modifiers.ForEach(modifier => modifier(obj));

            return (T)obj;
        }
    }

    class ObjectSetup<T>
    {

        private readonly ObjectGraph _graph;

        public ObjectSetup(ObjectGraph graph)
        {
            _graph = graph;
        }

        public ObjectSetup<T> Returns()
        {
            _graph.SetCreation(() => Activator.CreateInstance(typeof(T)));
            return this;
        }

        public ObjectSetup<T> OnCreation(Action<T> action)
        {
            _graph.AddModifier(obj => action.Invoke((T)obj));
            return this;
        }
    }
}
