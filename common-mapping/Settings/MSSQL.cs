using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common_mapping.Settings
{
    public class MSSQL
    {
        public string ConnectionString { get; set; }
        public string Schema { get; set; } = "mapping";
        public string TableName_MapType { get; set; } = "types";
        public string TableName_MapLink { get; set; } = "links";
        public string TableName_MapItem { get; set; } = "items";
    }
}
