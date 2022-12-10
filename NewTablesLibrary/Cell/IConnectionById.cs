using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    internal interface IConnectionById
    {
        int ValueID { get; }
        void LoadConnection();
    }
}
