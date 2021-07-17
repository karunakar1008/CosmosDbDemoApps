using System;
using System.Data.SqlClient;

namespace AzureSQLDbDemoApp
{
    class Program
    {
        //Create Sql database in azure
        //Set fire wall
        //Create Employee table in the database
        // Copy connection string here

        static string connectionstring = "";

        static void Main(string[] args)
        {
            connectionstring = "Server=tcp:az-newsqldbserver.database.windows.net,1433;Initial Catalog=csndb;Persist Security Info=False;User ID=karunakar1001;Password=Karuna@1001*1002;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var conn = new SqlConnection(connectionstring);
            var cmd = new SqlCommand("INSERT Employee (FirstName, LastName) VALUES (@FirstName, @LastName)", conn);
            EmployeeEntity emp = new EmployeeEntity() { FirstName="Karunakar", LastName= "Bhogyari" };
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.WriteLine("Employee data inserted successfully");
        }
    }

    public class EmployeeEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
    }
}
