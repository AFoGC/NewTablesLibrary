// See https://aka.ms/new-console-template for more information
using NewTablesLibrary;

Command command = new Command();
command.GetCommand("    <name: Simple> ///");
Console.WriteLine(command.FieldName + "|");
Console.WriteLine(command.FieldValue + "|");
