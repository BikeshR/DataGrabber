using System;

namespace DataGrabber.Models
{
    public class TestModel
    {
        public DateTime TradeTime { get; set; }
        public Decimal Open { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Close { get; set; }
        public int Volume { get; set; }
    }
}
