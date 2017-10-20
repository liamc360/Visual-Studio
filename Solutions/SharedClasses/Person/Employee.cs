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
    public class Employee
    {
        public int EmpNo { get; set; }

        public string BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string HireDate { get; set; }

        public Employee()
        {

        }

        public static List<Employee> GetEmployees(int empNo = 0, string birthDate = "", string firstName = "", string lastName = "", string gender = "", string hireDate = "")
        {
            List<Employee> employees = new List<Employee> { };
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(SqlConnect.GetConString()))
            {
                string sCommand = string.Empty;
                string sAnd = "";

                sCommand += "SELECT * FROM Website..Employees WHERE ";


                if(empNo != 0)
                {
                    sCommand += " EmpNo = " + empNo;
                    sAnd = " AND ";
                }
                if (firstName != "")
                {
                    sCommand += " FirstName = " + firstName;
                    sAnd = " AND ";
                }

                if (gender != "")
                {
                    sCommand += " Gender = '" + gender + "'";
                    sAnd = " AND ";
                }

                try
                {
                    SqlCommand command = new SqlCommand(sCommand, con);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int colIndex = 0;
                            Employee e = new Employee();

                            colIndex = reader.GetOrdinal("EmpNo");
                            e.EmpNo = reader.GetInt32(colIndex);

                            colIndex = reader.GetOrdinal("BirthDate");
                            DateTime x = reader.GetDateTime(colIndex);
                            string y = x.ToString();
                            e.BirthDate = y;

                            colIndex = reader.GetOrdinal("FirstName");
                            e.FirstName = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("LastName");
                            e.LastName = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("Gender");
                            e.Gender = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("HireDate");
                            x = reader.GetDateTime(colIndex);
                            y = x.ToString();
                            e.HireDate = y;
                            employees.Add(e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return employees;
        }
    }
}
