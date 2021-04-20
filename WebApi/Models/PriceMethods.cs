using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PriceMethods
    {
        public int InsertPrice(int Cu_Id, int price, out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en skribent i databasen
            String sqlstring = "INSERT INTO Tbl_Price ( Pr_Price, Pr_Customer) VALUES (@Price, @Cu_Id)";

            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            dbCommand.Parameters.Add("Cu_Id", SqlDbType.Int).Value = Cu_Id;
            dbCommand.Parameters.Add("Price", SqlDbType.Int).Value = price;



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
    }
}
