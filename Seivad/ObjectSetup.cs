using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    public class ObjectSetup<T>
    {

        private readonly ObjectGraph _graph;

        internal ObjectSetup(ObjectGraph graph)
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
