using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgamesDB.Models
{
    public class CustomerMethods
    {
        public int InsertCustomer(CustomerDetails cd, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "INSERT INTO Tbl_Customer ( Cu_Firstname, Cu_Lastname, Cu_Town, Cu_Address, Cu_Zipcode, Cu_Email, Cu_Password) " +
                "VALUES (@Cu_Firstname, @Cu_Lastname, @Cu_Town, @Cu_Address, @Cu_Zipcode, @Cu_Email, @Cu_Password) SELECT SCOPE_IDENTITY()";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Cu_Firstname", SqlDbType.NVarChar, 20).Value = cd.Cu_Firstname;
            dbCommand.Parameters.Add("Cu_Lastname", SqlDbType.NVarChar, 20).Value = cd.Cu_Lastname;
            dbCommand.Parameters.Add("Cu_Town", SqlDbType.NVarChar, 20).Value = cd.Cu_Town;
            dbCommand.Parameters.Add("Cu_Address", SqlDbType.NVarChar, 30).Value = cd.Cu_Address;
            dbCommand.Parameters.Add("Cu_Zipcode", SqlDbType.NVarChar, 30).Value = cd.Cu_Zipcode;
            dbCommand.Parameters.Add("Cu_Email", SqlDbType.NVarChar, 30).Value = cd.Cu_Email;
            dbCommand.Parameters.Add("Cu_Password", SqlDbType.NVarChar, 30).Value = cd.Cu_Password;


            try
            {
                dbConnection.Open();
                int i = 0;
                i = Convert.ToInt32(dbCommand.ExecuteScalar());
                if (i != 0)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Det skapades ingen avnändare";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }


        public int LoginCustomer(CustomerDetails cd, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "SELECT * FROM Tbl_Customer WHERE (Cu_Email = @Cu_Email AND Cu_Password = @Cu_Password)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //Skapa en adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            dbCommand.Parameters.Add("Cu_Email", SqlDbType.NVarChar, 30).Value = cd.Cu_Email;
            dbCommand.Parameters.Add("Cu_Password", SqlDbType.NVarChar, 30).Value = cd.Cu_Password;


            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "Customer");
                int count = 0;
                int i = 0;

                count = myDS.Tables["Customer"].Rows.Count;

                if (count > 0)
                {
                    cd.Cu_Id = Convert.ToInt16(myDS.Tables["Customer"].Rows[i]["Cu_Id"]);
                    cd.Cu_Firstname = myDS.Tables["Customer"].Rows[i]["Cu_Firstname"].ToString();
                    cd.Cu_Lastname = myDS.Tables["Customer"].Rows[i]["Cu_Lastname"].ToString();
                    cd.Cu_Town = myDS.Tables["Customer"].Rows[i]["Cu_Town"].ToString();
                    cd.Cu_Address = myDS.Tables["Customer"].Rows[i]["Cu_Address"].ToString();
                    cd.Cu_Zipcode = myDS.Tables["Customer"].Rows[i]["Cu_Zipcode"].ToString();
                    
                    errormsg = "";
                    return cd.Cu_Id;
                }
                else
                {
                    errormsg = "Wrong password or email";
                    return 0;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }


        public CustomerDetails GetCustomer(int Cu_Id, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "SELECT * FROM Tbl_Customer WHERE (Cu_Id = @Cu_Id)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //Skapa en adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            CustomerDetails cd = new CustomerDetails();

            dbCommand.Parameters.Add("Cu_Id", SqlDbType.Int).Value = Cu_Id;


            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "Customer");
                int count = 0;
                int i = 0;

                count = myDS.Tables["Customer"].Rows.Count;

                if (count > 0)
                {
                    cd.Cu_Id = Convert.ToInt16(myDS.Tables["Customer"].Rows[i]["Cu_Id"]);
                    cd.Cu_Firstname = myDS.Tables["Customer"].Rows[i]["Cu_Firstname"].ToString();
                    cd.Cu_Lastname = myDS.Tables["Customer"].Rows[i]["Cu_Lastname"].ToString();
                    cd.Cu_Town = myDS.Tables["Customer"].Rows[i]["Cu_Town"].ToString();
                    cd.Cu_Address = myDS.Tables["Customer"].Rows[i]["Cu_Address"].ToString();
                    cd.Cu_Zipcode = myDS.Tables["Customer"].Rows[i]["Cu_Zipcode"].ToString();
                    cd.Cu_Email = myDS.Tables["Customer"].Rows[i]["Cu_Email"].ToString();
                    cd.Cu_Password = myDS.Tables["Customer"].Rows[i]["Cu_Password"].ToString();

                    errormsg = "";
                    return cd;
                }
                else
                {
                    errormsg = "ingen med det id";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public List<CustomerDetails> GetCustomers(out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "SELECT * FROM Tbl_Customer";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //Skapa en adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<CustomerDetails> CustomerList = new List<CustomerDetails>();

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "Customer");
                int count = 0;
                int i = 0;

                count = myDS.Tables["Customer"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        CustomerDetails cd = new CustomerDetails();
                        cd.Cu_Id = Convert.ToInt16(myDS.Tables["Customer"].Rows[i]["Cu_Id"]);
                        cd.Cu_Firstname = myDS.Tables["Customer"].Rows[i]["Cu_Firstname"].ToString();
                        cd.Cu_Lastname = myDS.Tables["Customer"].Rows[i]["Cu_Lastname"].ToString();
                        cd.Cu_Town = myDS.Tables["Customer"].Rows[i]["Cu_Town"].ToString();
                        cd.Cu_Address = myDS.Tables["Customer"].Rows[i]["Cu_Address"].ToString();
                        cd.Cu_Zipcode = myDS.Tables["Customer"].Rows[i]["Cu_Zipcode"].ToString();
                        cd.Cu_Email = myDS.Tables["Customer"].Rows[i]["Cu_Email"].ToString();
                        cd.Cu_Password = myDS.Tables["Customer"].Rows[i]["Cu_Password"].ToString();

                        i++;
                        CustomerList.Add(cd);
                    }
                    errormsg = "";
                    return CustomerList;
                }
                else
                {
                    errormsg = "no products, something went wrong";
                    return null;
                }
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int UpdateCustomer(CustomerDetails cd, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "UPDATE Tbl_Customer SET Cu_Firstname = @Cu_Firstname, Cu_Lastname = @Cu_Lastname, " +
                "Cu_Town = @Cu_Town, Cu_Address = @Cu_Address, " +
                "Cu_Zipcode = @Cu_Zipcode, Cu_Email = @Cu_Email, Cu_Password = @Cu_Password WHERE Cu_Id = @Cu_Id";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Cu_Id", SqlDbType.Int).Value = cd.Cu_Id;
            dbCommand.Parameters.Add("Cu_Firstname", SqlDbType.NVarChar, 20).Value = cd.Cu_Firstname;
            dbCommand.Parameters.Add("Cu_Lastname", SqlDbType.NVarChar, 20).Value = cd.Cu_Lastname;
            dbCommand.Parameters.Add("Cu_Town", SqlDbType.NVarChar, 20).Value = cd.Cu_Town;
            dbCommand.Parameters.Add("Cu_Address", SqlDbType.NVarChar, 30).Value = cd.Cu_Address;
            dbCommand.Parameters.Add("Cu_Zipcode", SqlDbType.NVarChar, 30).Value = cd.Cu_Zipcode;
            dbCommand.Parameters.Add("Cu_Email", SqlDbType.NVarChar, 30).Value = cd.Cu_Email;
            dbCommand.Parameters.Add("Cu_Password", SqlDbType.NVarChar, 30).Value = cd.Cu_Password;


            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                
                if (i == 1)
                { 
                    errormsg = ""; 
                }
                else 
                { 
                    errormsg = "kunde inte ändra"; 
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally 
            { 
                dbConnection.Close(); 
            }

        }

        public int DeleteCustomer(int Cu_Id, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect " +
                "Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en user i databasen
            String sqlstring = "DELETE FROM Tbl_Customer WHERE Cu_Id = @Cu_Id";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Cu_Id", SqlDbType.Int).Value = Cu_Id;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1)
                {
                    errormsg = "";
                }
                else
                {
                    errormsg = "Raderas inget";
                }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}

 