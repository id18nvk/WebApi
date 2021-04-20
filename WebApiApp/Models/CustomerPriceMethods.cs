using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.Models;

namespace WebApiApp.Models
{
    public class CustomerPriceMethods
    {
        public int GetAveragePrice(List<CustomerPriceDetails> Customers)
        {
            int price = 0;
            for (int i = 0; i < Customers.Count; i++)
            {
                price += Customers[i].Pr_Price;
            }
            price = price / Customers.Count;
            return price;
        }
    }
}
