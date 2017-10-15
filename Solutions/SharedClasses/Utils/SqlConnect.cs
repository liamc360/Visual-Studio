using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SharedClasses.Utils
{
    public class SqlConnect
    {
        public static string GetConString(string name)
        {
            return ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString;
        }
    }
    
}
