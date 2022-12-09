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
        public static bool HasGenericTypeDefenition(this Type genericType, Type genericTypeDefenition)
        {
            if (genericType.IsGenericType)
                if (genericType.GetGenericTypeDefinition() == genericTypeDefenition) 
                    return true;

            return false;
        }
    }
}
