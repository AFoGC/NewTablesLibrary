using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    internal static class TypeExtension
    {
        internal static IEnumerable<FieldInfo> GetAllFields(this Type type, BindingFlags flags)
        {
            Type current = type;
            while (current != null)
            {
                foreach (FieldInfo field in current.GetFields(flags))
                {
                    yield return field;
                }
                current = current.BaseType;
            }
        }
    }
}
