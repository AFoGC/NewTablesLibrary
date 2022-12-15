using Xunit;

namespace NewTablesLibrary.Tests
{
    public class CommandTests
    {
        [Fact]
        public void TestDefaultCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("    <name: Simple> ///");

            Assert.Equal("name", command.FieldName);
            Assert.Equal("Simple", command.FieldValue);
            Assert.True(command.IsCommand);
            Assert.True(isCommand);
        }

        [Fact]
        public void TestMarkCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("<Marka>");

            Assert.Equal("Marka", command.FieldName);
            Assert.True(command.IsMark);
            Assert.True(command.IsCommand);
            Assert.True(isCommand);
        }

        [Fact]
        public void TestWrongCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("Ada: fa");

            Assert.False(isCommand);
            Assert.False(command.IsCommand);
        }
    }
}
