using DataGrabber.Data;
using DataGrabber.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DataGrabber
{
    public class Program
    {
        public static async Task Main()
        {
            while(true)
            {
                DailyPriceContext day = new DailyPriceContext();
                ApiCall api = new ApiCall(day);
                var jsonString = await api.GetJson();
                await api.UpdateDatabase(jsonString);
                Thread.Sleep(60000);
            }
        }
    }
}
