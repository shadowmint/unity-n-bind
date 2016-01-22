using System.Collections.Generic;
using System.Reflection;
using System;

namespace N.Package.Bind.Core
{
    public static class TypeExtensions
    {
        /// Yield the set of public properties with set methods on the type
        public static IEnumerable<PropertyInfo> GetSetProperties(this Type self)
        {
            foreach (var pp in self.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                N._.Log("properties... {0}", pp);
                if (pp.CanWrite)
                {
                    yield return pp;
                }
            }
        }
    }
}
