using System;
using System.Collections.Generic;

namespace AlphaMvc.Models
{
 

    public class SearchModel
    {
        public string Ticker { get; set; } = "";

        public int PageNr { get; set; } = 1;
        public int PageSize { get; set; } = 10;
       

        public int TotalRecords { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public List<AlphaVantagePrice> Results { get; set; } = new List<AlphaVantagePrice>();
        public string ErrorMessage { get; set; } = "";
    }

}