using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Website
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sqlDbConn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ToString();

            
        }
        private static void Test()
        {
            SqlConnection connection = new SqlConnection("a");
        }
    }
}