using SharedClasses.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SharedClasses.Person
{
    public class User
    {
        public User()
        {

        }

        public int AddUser()
        {
            int rowsAdded = 0;
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(SqlConnect.GetConString("General")))
            {
                SqlCommand comTask = new SqlCommand();
                comTask.Connection = con;
                String sCommand = String.Empty;
                sCommand += "INSERT INTO General..UserAccounts (";
                sCommand += "FirstName,";
                sCommand += "LastName,";
                sCommand += "EmailAddress,";
                sCommand += "Password,";
                sCommand += "PasswordSalt,";
                sCommand += "LastIP,";
                sCommand += "DateCreated,";
                sCommand += "MobileNumber";
                sCommand += " )";
                sCommand += " VALUES (";
                sCommand += "@FirstName" + ",";
                sCommand += "@LastName" + ",";
                sCommand += "@EmailAddress" + ",";
                sCommand += "@Password" + ",";
                sCommand += "@PasswordSalt" + ",";
                sCommand += "@LastIP" + ",";
                sCommand += "@DateCreated" + ",";
                sCommand += "@MobileNumber";
                sCommand += " );";

                comTask.CommandText = sCommand;
                comTask.CommandType = CommandType.Text;

                comTask.Parameters.Add("@FirstName", SqlDbType.VarChar);
                comTask.Parameters["@FirstName"].Value = FirstName;

                comTask.Parameters.Add("@LastName", SqlDbType.VarChar);
                comTask.Parameters["@LastName"].Value = LastName;

                comTask.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                comTask.Parameters["@EmailAddress"].Value = EmailAddress;

                comTask.Parameters.Add("@Password", SqlDbType.VarChar);
                comTask.Parameters["@Password"].Value = Password;

                comTask.Parameters.Add("@PasswordSalt", SqlDbType.VarChar);
                comTask.Parameters["@PasswordSalt"].Value = PasswordSalt;

                comTask.Parameters.Add("@LastIP", SqlDbType.VarChar);
                comTask.Parameters["@LastIP"].Value = LastIP;

                comTask.Parameters.Add("@DateCreated", SqlDbType.DateTime);
                comTask.Parameters["@DateCreated"].Value = DateTime.Now;

                comTask.Parameters.Add("@MobileNumber", SqlDbType.VarChar);
                comTask.Parameters["@MobileNumber"].Value = MobileNumber;

                try
                {
                    con.Open();
                    rowsAdded = comTask.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAdded);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return rowsAdded;
        }





        private bool CheckPassword(string hashedPassword, string password, string salt)
        {
            String newHashedPassword = Security.HashPassword(password, salt);
            if (hashedPassword.Equals(newHashedPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int accountid;  // the name field
        public int AccountID    // the Name property
        {
            get
            {
                return accountid;
            }
        }

        private string firstname;  // the name field
        public string FirstName    // the Name property
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        private string lastname;  // the name field
        public string LastName    // the Name property
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        private string emailaddress;  // the name field
        public string EmailAddress    // the Name property
        {
            get
            {
                return emailaddress;
            }
            set
            {
                emailaddress = value;
            }
        }

        private bool active;  // the name field
        public bool Active    // the Name property
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        private string password;  // the name field
        public string Password    // the Name property
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private string passwordsalt;  // the name field
        public string PasswordSalt    // the Name property
        {
            get
            {
                return passwordsalt;
            }
            set
            {
                passwordsalt = value;
            }
        }

        private string lastip;  // the name field
        public string LastIP    // the Name property
        {
            get
            {
                return lastip;
            }
            set
            {
                lastip = value;
            }
        }

        private DateTime datecreated;  // the name field
        public DateTime DateCreated    // the Name property
        {
            set
            {
                datecreated = value;
            }
        }

        private string mobilenumber;  // the name field
        public string MobileNumber    // the Name property
        {
            get
            {
                return mobilenumber;
            }
            set
            {
                mobilenumber = value;
            }
        }

        private int accounttype;  // the name field
        public int AccountType    // the Name property
        {
            get
            {
                return accounttype;
            }
            set
            {
                accounttype = value;
            }
        }
    }
}
