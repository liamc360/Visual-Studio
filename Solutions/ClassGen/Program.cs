using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassGen
{
    class Program
    {
        SqlConnection conn = new SqlConnection(SharedClasses.Utils.SqlConnect.GetConString());

        //Microsoft.SqlServer.Server.


            //        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



            //Data Source = (LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Website.mdf;Initial Catalog = Website; Integrated Security = True






        static void Main(string[] args)
        {
        }
    }
}
