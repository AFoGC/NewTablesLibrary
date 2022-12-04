using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    public class Command
    {
        private string _fieldName;
        private string _fieldValue;
        private bool _isCommand;
        private bool _isMark;

        public Command()
        {

        }

        public string FieldName => _fieldName;
        public string FieldValue => _fieldValue;
        public bool IsCommand => _isCommand;
        public bool IsMark => _isMark;

        public bool GetCommand(string line)
        {
            string fieldName = string.Empty;
            string fieldValue = string.Empty;
            int startOfCommand = line.IndexOf('<');
            int endOfCommmand = line.IndexOf('>');
            int separatorInxex = line.IndexOf(':');

            if (startOfCommand == -1 || endOfCommmand == -1)
            {
                SetWrongCommand();
                return false;
            }

            startOfCommand += 1;

            if (separatorInxex == -1)
            {
                fieldName = line.Substring(startOfCommand, endOfCommmand - startOfCommand);
                SetMark(fieldName);
            }
            else
            {
                fieldName = line.Substring(startOfCommand, separatorInxex - startOfCommand);

                separatorInxex += 2;
                fieldValue = line.Substring(separatorInxex, endOfCommmand - separatorInxex);
                SetFullCommand(fieldName, fieldValue);
            }

            return true;
        }

        private void SetFullCommand(string fieldName, string fieldValue)
        {
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _isMark = false;
            _isCommand = true;
        }

        private void SetMark(string fieldName)
        {
            _fieldName = fieldName;
            _fieldValue = string.Empty;
            _isMark = true;
            _isCommand = true;
        }

        private void SetWrongCommand()
        {
            _fieldName = string.Empty;
            _fieldValue = string.Empty;
            _isCommand = false;
            _isMark = false;
        }
    }
}
