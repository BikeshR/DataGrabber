using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataGrabber.Model;
using DataGrabber.Data;

namespace DataGrabber
{
    public class ApiCall
    {
        private readonly DailyPriceContext _context;

        public ApiCall(DailyPriceContext context)
        {
            _context = context;
        }

        public async Task<string> GetJson()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=compact&apikey=CX04WU00KT84WP7F");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            return responseBody;
        }

        public async Task SaveSomething()
        {
            DailyPrice test = new DailyPrice()
            {
                Symbol = "ASD",
                PriceDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                Open = 1.2345M,
                High = 2.4354M,
                Low = 3.4343M,
                Close = 4.3234M,
                AdjClose = 4.2123M,
                Volume = 400,
                DividendAmount = 3.4535M,
                SplitCoefficient = 2.5345M
            };
            _context.DailyPrices.Add(test);
            await _context.SaveChangesAsync();
        }

        



        //public static List<DailyPrice> ConvertJson<DailyPrice>(string jsonString)
        //{
        //    JObject jsonObject = JObject.Parse(jsonString);

        //    //List<DailyPrice> rows = new List<DailyPrice>();
        //    //var dailyPrice = new DailyPrice()
        //    //{
        //    //    Symbol = "",

        //    //};
        //    //rows.Add(dailyPrice);

        //    ////var rows = jsonObject["Time Series (Daily)"].Children().ToList();
        //    //return rows;
        //}



    }
}
