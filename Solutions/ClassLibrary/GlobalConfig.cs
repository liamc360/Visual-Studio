using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace clGeneral
{
    public static class GlobalConfig
    {
        //public static IDataConnection Connections { get; private set; } = new List<IDataConnection>();
        private static void Test()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
            }
        }
       
    }
}
