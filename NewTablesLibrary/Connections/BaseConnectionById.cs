using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTablesLibrary
{
    public abstract class BaseConnectionById : ILoadField
    {
        public abstract void FromString(string value);
        internal abstract void LoadConnection();
    }
}
