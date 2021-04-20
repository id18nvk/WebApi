using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgamesDB.Models
{
    public class CustomerDetails
    {
        public CustomerDetails() { }

        public int Cu_Id { get; set; }
        public string Cu_Firstname { get; set; }
        public string Cu_Lastname { get; set; }
        public string Cu_Town { get; set; }
        public string Cu_Address { get; set; }
        public string Cu_Zipcode { get; set; }
        public string Cu_Email { get; set; }
        public string Cu_Password { get; set; }



    }
}
