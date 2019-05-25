using System;

namespace DataGrabber.Models
{
    public class DailyPrice
    {
        public string Symbol { get; set; }
        public DateTime PriceDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Decimal Open { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Close { get; set; }
        public Decimal AdjClose { get; set; }
        public int Volume { get; set; }
        public Decimal DividendAmount { get; set; }
        public Decimal SplitCoefficient { get; set;
}
