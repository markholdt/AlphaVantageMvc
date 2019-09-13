using System;
using System.Collections.Generic;

namespace AlphaMvc.Models
{
 

    public class SearchModel
    {
        public string Ticker { get; set; } = "";

        public List<AlphaVantagePrice> Prices { get; set; } = new List<AlphaVantagePrice>();
        public string ErrorMessage { get; set; } = "";
    }

}