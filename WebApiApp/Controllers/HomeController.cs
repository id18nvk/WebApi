using BoardgamesDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiApp.Helper;
using WebApiApp.Models;


namespace WebApiApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        readonly CustomerAPI _api = new CustomerAPI();
       
        public async Task<IActionResult> Index()
        {
            List<CustomerPriceDetails> Customers = new List<CustomerPriceDetails>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Values");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                Customers = JsonConvert.DeserializeObject<List<CustomerPriceDetails>>(result);
            }
            return View(Customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            CustomerDetails Customer = new CustomerDetails();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Values/" + id.ToString());

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                Customer = JsonConvert.DeserializeObject<CustomerDetails>(result);
            }
            return View(Customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CustomerDetails Customers = new CustomerDetails();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Values/" + id.ToString());

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> Edit(int id)
        {
            CustomerDetails Customer = new CustomerDetails();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Values/" + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    Customer = JsonConvert.DeserializeObject<CustomerDetails>(result);
                }
            return View(Customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDetails Customer)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PutAsJsonAsync<CustomerDetails>("api/Values", Customer);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(Customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDetails Customer)
        {

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PostAsJsonAsync<CustomerDetails>("api/Values", Customer);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(Customer);
        } 


        //RÄKNA UT PRIS
        public async Task<IActionResult> AveragePrice()
        {
            List<CustomerPriceDetails> Customers = new List<CustomerPriceDetails>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Values");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                Customers = JsonConvert.DeserializeObject<List<CustomerPriceDetails>>(result);
            }
            CustomerPriceMethods cpm = new CustomerPriceMethods();
            int price = cpm.GetAveragePrice(Customers);

            TempData["price"] = price;

            return RedirectToAction("Index");
        }





    }
}
