using System;
using System.ComponentModel.DataAnnotations;

namespace DataGrabber.Model
{
    public class DailyPrice
    {
        public int Id { get; set; }
        [Required]
        [StringLength(4)]
        public string Symbol { get; set; }
        [Required]
        public DateTime PriceDate { get; set; }
        [Required]
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