using SharedClasses.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SharedClasses.Person
{
    public class Employee
    {
        
        [Key]
        public int EmpNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public Employee()
        {

        }

        public static List<Employee> GetEmployees(int empNo = 0, string birthDate = "", string firstName = "", string lastName = "", string gender = "", string hireDate = "")
        {
            List<Employee> employees = new List<Employee> { };
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(SqlConnect.GetConString()))
            {
                string sCommand = "SELECT Top 2000 * FROM Website..Employees WHERE 1=1";

                if (empNo != 0)
                {
                    sCommand += " AND EmpNo = @empno";
                } 
                if (firstName != string.Empty)
                {
                    sCommand += " AND FirstName LIKE @firstname";
                }
                if (lastName != string.Empty)
                {
                    sCommand += " AND LastName LIKE @lastname";
                }
                if (gender != string.Empty)
                {
                    sCommand += " AND Gender = @gender";
                }

                try
                {
                    SqlCommand cmd = new SqlCommand(sCommand, con);
                    cmd.Parameters.Add("@empno", SqlDbType.Int).Value = empNo;
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = firstName + "%";
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastName + "%";
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = gender;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int colIndex = 0;
                            Employee e = new Employee();

                            colIndex = reader.GetOrdinal("EmpNo");
                            e.EmpNo = reader.GetInt32(colIndex);

                            colIndex = reader.GetOrdinal("BirthDate");
                            e.BirthDate = reader.GetDateTime(colIndex);

                            colIndex = reader.GetOrdinal("FirstName");
                            e.FirstName = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("LastName");
                            e.LastName = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("Gender");
                            e.Gender = reader.GetString(colIndex);

                            colIndex = reader.GetOrdinal("HireDate");
                            e.HireDate = reader.GetDateTime(colIndex);
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


        /*if (hireDate != string.Empty)
        {
            sCommand += " AND EmpNo = @empno";
        }
        if (birthDate != string.Empty)
        {
            sCommand += " AND EmpNo = @empno";
        }*/
        public static int AddEmployee(Employee e)
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(SqlConnect.GetConString()))
            {
                string sCommand = "INSERT INTO Website..Employees(BirthDate, FirstName, LastName, Gender, HireDate) VALUES(@birthdate, @firstname, @lastname, @gender, @hiredate)";

                try
                {
                    SqlCommand cmd = new SqlCommand(sCommand, con);
                    cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = e.BirthDate;
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = e.FirstName;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = e.LastName;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = e.Gender;
                    cmd.Parameters.Add("@hiredate", SqlDbType.Date).Value = e.HireDate;

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return 0;
        }

        public static int UpdateEmployee(Employee e)
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(SqlConnect.GetConString()))
            {
                string sCommand = "UPDATE Website..Employees SET BirthDate = @birthdate, FirstName = @firstname, LastName = @lastname, Gender = @gender, HireDate = @hiredate WHERE EmpNo = @empno";

                try
                {
                    SqlCommand cmd = new SqlCommand(sCommand, con);
                    cmd.Parameters.Add("@birthdate", SqlDbType.Date).Value = e.BirthDate;
                    cmd.Parameters.Add("@firstname", SqlDbType.VarChar).Value = e.FirstName;
                    cmd.Parameters.Add("@lastname", SqlDbType.VarChar).Value = e.LastName;
                    cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = e.Gender;
                    cmd.Parameters.Add("@hiredate", SqlDbType.Date).Value = e.HireDate;
                    cmd.Parameters.Add("@empno", SqlDbType.Int).Value = e.EmpNo;

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return 0;
        }
    }
}
