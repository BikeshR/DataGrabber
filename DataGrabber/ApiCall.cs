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

        //Takes the Json file and saves it to DailyPrices Table
        public async Task UpdateDatabase(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            List<DailyPrice> rows = new List<DailyPrice>();

            var jsonRows = jsonObject["Time Series (Daily)"].Children().ToList();
            foreach (var jsonRow in jsonRows)
            {
                JObject rowObject = JObject.Parse("{" + jsonRow.ToString() + "}");

                //Price Date extracted from the Key to be reused later
                var priceDate = rowObject.Properties().First().Name;

                //Get DailyPrice object to be saved to a row
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

                //If the row already exists, then update all data except Id
                if(_context.DailyPrices.Any(x => x.Symbol == dailyPrice.Symbol && x.PriceDate == dailyPrice.PriceDate))
                {
                    var existingRow = _context.DailyPrices.Where(x => x.Symbol == dailyPrice.Symbol && x.PriceDate == dailyPrice.PriceDate).FirstOrDefault();
                    existingRow.LastUpdatedDate = dailyPrice.LastUpdatedDate;
                    existingRow.Open = dailyPrice.Open;
                    existingRow.High = dailyPrice.High;
                    existingRow.Low = dailyPrice.Low;
                    existingRow.Close = dailyPrice.Close;
                    existingRow.AdjClose = dailyPrice.AdjClose;
                    existingRow.Volume = dailyPrice.Volume;
                    existingRow.DividendAmount = dailyPrice.DividendAmount;
                    existingRow.SplitCoefficient = dailyPrice.SplitCoefficient;
                }
                else
                {
                    rows.Add(dailyPrice);
                }        
            }

            _context.DailyPrices.AddRange(rows);
            await _context.SaveChangesAsync();
        }
    }
}
