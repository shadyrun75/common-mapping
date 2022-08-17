using common_mapping.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common_mapping.Settings
{

    public class Setup
    {
        public ConnectionType ConnectionType { get; set; } 
        public SQLite SQLite { get; set; }
        public MSSQL MSSQL { get; set; }
        public API API { get; set; }    
    }
}
