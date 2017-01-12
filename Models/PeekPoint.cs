using System;
using System.Collections.Generic;
using System.Text;

namespace Warensoft.EntLib.StockServiceClient.Models
{
    public enum PeekType
    {
        Top,
        Bottom
    }
    public class PeekPoint
    {
        public PeekType Type { get; set; }
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public double Time { get; set; }
        
        public int Index { get; internal set; }
    }
}
