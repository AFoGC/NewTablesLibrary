using System;
using System.Collections.Generic;
using System.Reflection;

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

        public static IEnumerable<T> GetFieldsWithType<T>(this Type type, object obj) where T : class
        {
            Type fieldType = typeof(T);
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (FieldInfo fieldInfo in type.GetFields(flags))
                if (fieldInfo.FieldType == fieldType)
                    yield return fieldInfo.GetValue(obj) as T;
        }

        internal static IEnumerable<FieldInfo> GetAllFields(this Type type, BindingFlags flags)
        {
            List<Type> types = new List<Type>();
            Type current = type;
            while (current != null)
            {
                types.Insert(0, current);
                current = current.BaseType;
            }

            foreach (Type item in types)
                foreach (FieldInfo field in item.GetFields(flags))
                    yield return field;
        }
    }
}
