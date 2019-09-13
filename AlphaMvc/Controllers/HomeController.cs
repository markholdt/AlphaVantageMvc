using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaMvc.Models;
using ServiceStack;

namespace AlphaMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string ticker = "")
        {

            var response = new SearchModel {Ticker = ticker};
            // retrieve monthly prices for Microsoft
            if (!String.IsNullOrEmpty(ticker))
            {

                var apiKey = "demo"; // retrieve your api key from https://www.alphavantage.co/support/#api-key
               
                var apiResponse =
                    $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={ticker}&apikey={apiKey}&datatype=csv"
                        .GetStringFromUrl();
                try
                {
                    response.Prices = apiResponse.FromCsv<List<AlphaVantagePrice>>().OrderByDescending(ar => ar.Timestamp).Take(10).ToList();
                }
                catch (Exception exc)
                {
                    response.ErrorMessage = apiResponse.FromJson<AlphaVantageError>().Information;
                }
            }

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
