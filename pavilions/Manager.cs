using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.SqlClient;

namespace pavilions {
    class Manager {
        public static Frame MainFrame { get; set; } // frame
        public static SqlConnection connection { get; set; } // connection
    }
}
