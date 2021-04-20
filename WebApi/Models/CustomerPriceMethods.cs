using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class CustomerPriceMethods
    {
        public List<CustomerPriceDetails> GetCustomerPrice(out string errormsg)
        {
            //SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //koppling
            dbConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardgamesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //sqlstring och lägg till en user i databasen


            String sqlstring = "SELECT Tbl_Customer.Cu_Id, Tbl_Customer.Cu_Firstname, Tbl_Customer.Cu_Lastname, Tbl_Customer.Cu_Email, " +
                "Tbl_Price.Pr_Price FROM(Tbl_Customer INNER JOIN Tbl_Price ON Tbl_Customer.Cu_Id = Tbl_Price.Pr_Customer)";
            
            SqlCommand dbCommand = new SqlCommand(sqlstring, dbConnection);

            //Skapa en adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<CustomerPriceDetails> CustomerPriceList = new List<CustomerPriceDetails>();

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "CustomerPrice");
                int count = 0;
                int i = 0;

                count = myDS.Tables["CustomerPrice"].Rows.Count;

                if (count > 0)
                {
                    while (i < count)
                    {
                        CustomerPriceDetails cpd = new CustomerPriceDetails();
                        cpd.Cu_Id = Convert.ToInt16(myDS.Tables["CustomerPrice"].Rows[i]["Cu_Id"]);
                        cpd.Cu_Firstname = myDS.Tables["CustomerPrice"].Rows[i]["Cu_Firstname"].ToString();
                        cpd.Cu_Lastname = myDS.Tables["CustomerPrice"].Rows[i]["Cu_Lastname"].ToString();
                        cpd.Cu_Email = myDS.Tables["CustomerPrice"].Rows[i]["Cu_Email"].ToString();
                        cpd.Pr_Price = Convert.ToInt16(myDS.Tables["CustomerPrice"].Rows[i]["Pr_Price"]);




                        i++;
                        CustomerPriceList.Add(cpd);
                    }
                    errormsg = "";
                    return CustomerPriceList;
                }
                else
                {
                    errormsg = "Det hämtas ingen artikel ämne kopplingar";
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
    }
}
