using System;

namespace DataGrabber.Model
{
    public class DailyPrice
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjClose { get; set; }
        public int Volume { get; set; }
        public decimal DividendAmount { get; set; }
        public decimal SplitCoefficient { get; set; }
    }
}