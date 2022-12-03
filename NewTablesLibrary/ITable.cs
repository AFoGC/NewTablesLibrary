using System;

namespace NewTablesLibrary
{
    public interface ITable
    {
        int ID { get; }
        string Name { get; set; }
        int Count { get; }
        Type DataType { get; }
    }
}
