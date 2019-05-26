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

        public async Task SaveToDatabase(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            List<DailyPrice> rows = new List<DailyPrice>();

            var jsonRows = jsonObject["Time Series (Daily)"].Children().ToList();
            foreach (var jsonRow in jsonRows)
            {
                JObject rowObject = JObject.Parse("{" + jsonRow.ToString() + "}");

                var priceDate = rowObject.Properties().First().Name;

                DailyPrice dailyPrice = new DailyPrice()
                {
                    Symbol = jsonObject["Meta Data"]["2. Symbol"].ToString(),
                    PriceDate = DateTime.Parse(priceDate),
                    LastUpdatedDate = DateTime.Parse(jsonObject["Meta Data"]["3. Last Refreshed"].ToString()),
                    Open = decimal.Parse(rowObject[priceDate]["1. open"].ToString()),
                    High = decimal.Parse(rowObject[priceDate]["2. high"].ToString()),
                    Low = decimal.Parse(rowObject[priceDate]["3. low"].ToString()),
                    Close = decimal.Parse(rowObject[priceDate]["4. close"].ToString()),
                    AdjClose = decimal.Parse(rowObject[priceDate]["5. adjusted close"].ToString()),
                    Volume = int.Parse(rowObject[priceDate]["6. volume"].ToString()),
                    DividendAmount = decimal.Parse(rowObject[priceDate]["7. dividend amount"].ToString()),
                    SplitCoefficient = decimal.Parse(rowObject[priceDate]["8. split coefficient"].ToString())
                };
                rows.Add(dailyPrice);
            }
            _context.DailyPrices.AddRange(rows);
            await _context.SaveChangesAsync();
        }
    }
}
