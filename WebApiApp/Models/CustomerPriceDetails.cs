using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApp.Models
{
    public class CustomerPriceDetails
    {
        public CustomerPriceDetails() { }
        public int Cu_Id { get; set; }
        public string Cu_Firstname { get; set; }
        public string Cu_Lastname { get; set; }
        public string Cu_Email { get; set; }
        public int Pr_Price { get; set; }
    }
}
