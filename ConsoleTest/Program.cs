using NewTablesLibrary;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleTest;

public class Program
{
    public static void Main()
    {
        ConnectionsFields.TestConnections();
    }

    private static IEnumerable<FieldInfo> AllTypeFields(Type type)
    {
        Type current = type;
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        while (current != null)
        {
            foreach (FieldInfo field in current.GetFields(flags))
            {
                yield return field;
            }
            current = current.BaseType;
        }
    }

    class O
    {
        private int pointer;
        public int Pointer => pointer;
    }

    class A : O
    {
        private int id;
        public int ID => id;
    }

    class B : A
    {
        private string Name = String.Empty;
    }
}



