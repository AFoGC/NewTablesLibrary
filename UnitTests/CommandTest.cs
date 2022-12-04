using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewTablesLibrary;
using System;
using System.Windows.Input;

namespace UnitTests
{
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void TestDefaultCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("    <name: Simple> ///");

            Assert.AreEqual("name", command.FieldName);
            Assert.AreEqual("Simple", command.FieldValue);
            Assert.IsTrue(command.IsCommand);
            Assert.IsTrue(isCommand);
        }

        [TestMethod]
        public void TestMarkCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("<Marka>");

            Assert.AreEqual("Marka", command.FieldName);
            Assert.IsTrue(command.IsMark);
            Assert.IsTrue(command.IsCommand);
            Assert.IsTrue(isCommand);
        }

        [TestMethod]
        public void TestWrongCommand()
        {
            Command command = new Command();
            bool isCommand = command.GetCommand("Ada: fa");

            Assert.IsFalse(isCommand);
            Assert.IsFalse(command.IsCommand);
        }
    }
}
