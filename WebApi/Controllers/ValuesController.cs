using BoardgamesDB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class valuescontroller : ControllerBase
    {
       
        //--------------- GET CUSTOMERLIST -----------------
        // get: api/<valuescontroller>
        [HttpGet]
        public List<CustomerPriceDetails> getCustomers()
        {
            
            List<CustomerPriceDetails> cpl = new List<CustomerPriceDetails>();
            CustomerPriceMethods cpm = new CustomerPriceMethods();
            cpl = cpm.GetCustomerPrice(out string errormsg);
            return cpl;
        }


        //--------------- GET CUSTOMER -----------------
        // get api/<valuescontroller>/5
        [HttpGet("{id}")]
        public CustomerDetails getCustomer(int id)
        {
            CustomerDetails cd = new CustomerDetails();
            CustomerMethods cm = new CustomerMethods();

            cd = cm.GetCustomer(id, out string errormsg);


            return cd;
        }


        //--------------- CREATE CUSTOMER -----------------
        [HttpPost]
        public void Post([FromBody] CustomerDetails cd)
        {
            CustomerMethods cm = new CustomerMethods();        

            int Cu_Id = cm.InsertCustomer(cd,out string errormsg);

            PriceMethods pm = new PriceMethods();

            Random rand = new Random();

            // autogenerate price
            pm.InsertPrice(Cu_Id, rand.Next(501), out string errormsg1);     
        }


        //--------------- UPDATE CUSTOMER -----------------
        // PUT api/<ValuesController>/5
        [HttpPut]
        public void Put([FromBody] CustomerDetails cd)
        {
            CustomerMethods cm = new CustomerMethods();
            cm.UpdateCustomer(cd, out string errormsg);
        }

        //--------------- DELETE CUSTOMER -----------------
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CustomerMethods cm = new CustomerMethods();
            cm.DeleteCustomer(id, out string errormsg);
        }


    }
}
