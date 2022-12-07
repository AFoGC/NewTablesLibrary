using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    internal static class StaticHelper
    {
        internal static bool NextCommand(IEnumerator<string> enumerator, Command command)
        {
            bool isMoved = enumerator.MoveNext();

            if (isMoved)
                command.GetCommand(enumerator.Current);

            return isMoved;
        }

        internal static CellAttribute GetCellAttrbute(BaseTable table)
        {
            Type attributeType = typeof(CellAttribute);
            Attribute attribute = Attribute.GetCustomAttribute(table.DataType, attributeType);
            CellAttribute cellAttribute = attribute as CellAttribute;
            return cellAttribute;
        }

        internal static IEnumerable<(FieldInfo, SaveFieldAttribute)> GetFieldsWithAttribute(object obj)
        {
            SaveFieldAttribute fieldAttribute;
            Type objType = obj.GetType();

            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            FieldInfo[] fields = objType.GetFields(flags);

            foreach (FieldInfo field in fields)
            {
                fieldAttribute = field.GetCustomAttribute<SaveFieldAttribute>();
                if (fieldAttribute != null)
                    yield return (field, fieldAttribute);
            }
        }
    }
}
