using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataGrabber
{
    public static class ApiCall
    {
        public static async Task GetJson()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await ApiCall.GetJson();
            }).GetAwaiter().GetResult();


        }
    }
}
