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
        public IActionResult Index(string ticker = "", int pageNr =1, int pageSize =10)
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
                    var allPrices = apiResponse.FromCsv<List<AlphaVantagePrice>>().ToList();
                    
                    int totalRecords = allPrices.Count;
                    int totalPages = (totalRecords + pageSize - 1) / pageSize;

                    response.TotalPages = totalPages;
                    response.TotalRecords = totalRecords;

                    response.Results = allPrices.OrderByDescending(ar => ar.Timestamp).Skip((pageNr  -1) * pageSize).Take(pageSize).ToList();
                }
                catch (Exception exc)
                {
                    response.ErrorMessage = apiResponse.FromJson<AlphaVantageError>().Information;
                }
            }

            response.PageNr = pageNr;
            response.PageSize = pageSize;
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Download()
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
