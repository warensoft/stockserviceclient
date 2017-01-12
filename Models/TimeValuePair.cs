using System;

namespace Warensoft.EntLib.StockServiceClient.Models
{
    public class TimeValuePair
    { 
        public DateTime DateTime { get; set; } 
        public double Value { get; set; }
        public TimeValuePair()
        { }
        public TimeValuePair(DateTime time, double value)
        {
            this.DateTime = time;
            this.Value = value;
        }
    }
}
