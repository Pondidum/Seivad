using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    public interface IArguments
    {
        IArguments Add(string name, object value);
        IDictionary<string, object> ToDictionary();
        IList<string> Names();
        IList<object> Values();

        int Count { get; }

    }
}
