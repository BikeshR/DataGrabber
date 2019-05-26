using DataGrabber.Data;
using DataGrabber.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataGrabber
{
    public class Program
    {
        public static async Task Main()
        {
            DailyPriceContext day = new DailyPriceContext();
            ApiCall api = new ApiCall(day);
            await api.GetJson();
            await api.SaveSomething();
        }
    }
}
