using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class PriceDetails
    {
        public PriceDetails() { }

        public int Pr_Id { get; set; }
        public int Pr_Price { get; set; }
        public int Pr_Customer { get; set; }
    }
}
