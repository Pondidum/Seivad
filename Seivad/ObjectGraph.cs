using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
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

}
