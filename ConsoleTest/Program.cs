using NewTablesLibrary;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;
using static ConsoleTest.ConnectionsFields;

namespace ConsoleTest;

public class Program
{
    public static void Main()
    {
        ManyToOne<Genre, Book> OneToMany = new ManyToOne<Genre, Book>(null);
        Type type = typeof(ManyToOne<,>);
        Type genericType = typeof(int);
        Console.WriteLine(type.Name);
        Console.WriteLine(genericType.Name);
        if (type.IsGenericType)
            Console.WriteLine(type.GetGenericTypeDefinition());
    }

    private static IEnumerable<string> GetList()
    {
        yield return"A";
        yield return"B";
        yield return"C";
        yield break;
        Console.WriteLine("aa");
        yield return"D";
    }

    
}



