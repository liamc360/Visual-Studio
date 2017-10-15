using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using SharedClasses.Person;

namespace Website
{
    public partial class SiteMaster : MasterPage
    {
        private User newUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            String sqlDbConn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServer"].ToString();

            if(true) //check email exists
            {
                newUser = new User();
                newUser.FirstName = "Liam";
                newUser.LastName = "Collings";
                newUser.EmailAddress = "liamc4113@hotmail.co.uk";
                newUser.Active = true;

                string salt = SharedClasses.Utils.Security.GenerateSalt();
                string saltedPass = SharedClasses.Utils.Security.HashPassword("password1", salt);
                newUser.Password = saltedPass;
                newUser.PasswordSalt = salt;
                newUser.LastIP = GetUserIP();
                newUser.MobileNumber = "55656876565";
                newUser.AccountType = 1;
                int rowsAdded = newUser.AddUser();
                if (rowsAdded > 0)
                {
                    //added
                }
            }
        }
        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}