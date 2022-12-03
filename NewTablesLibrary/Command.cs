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

        private void SetCommand(string fieldName, string fieldValue)
        {
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _isCommand = true;

            if (_fieldValue == string.Empty)
                _isMark = true;

            if (_fieldName == string.Empty && _fieldValue == string.Empty)
            {
                _isCommand = false;
                _isMark = false;
            }
        }

        public void GetCommand()
        {

        }
    }
}
