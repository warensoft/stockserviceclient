using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Warensoft.EntLib.StockServiceClient.Models
{
    /// <summary>
    /// K线模型
    /// </summary>
    public class Kline
    {
        /// <summary>
        /// 开
        /// </summary>
        public double Open
        {
            get
            {
                return this.Data[1];
            }
        }
        /// <summary>
        /// 收
        /// </summary>
        public double Close
        {
            get
            {
                return this.Data[4];
            }
        }
        /// <summary>
        /// 高
        /// </summary>
        public double High
        {
            get
            {
                return this.Data[2];
            }
        }
        /// <summary>
        /// 低
        /// </summary>
        public double Low
        {
            get
            {
                return this.Data[3];
            }
        }
        /// <summary>
        /// 中线
        /// </summary>
        public double Middle
        {
            get
            {
                return (this.Data[1] + this.Data[4]) / 2;
            }
        }
        public double Upper
        {
            get
            {
                return (this.Max + this.Middle) / 2;
            }
        }
        public double Max
        {
            get
            {
                return this.Data[1] > Data[4] ? Data[1] : Data[4];
            }
        }
        public double Lower
        {
            get
            {
                return (this.Min + this.Middle) / 2;
            }
        }
        public double Min
        {
            get
            {
                return this.Data[1] < Data[4] ? Data[1] : Data[4];
            }
        }
        public DateTime Time
        {
            get
            {
                return new DateTime(1970, 1, 1) + TimeSpan.FromMilliseconds((double)this.Data[0]) + TimeSpan.FromHours(8);
            }
        }
        public bool IsRed
        {
            get
            {
                return this.Data[1] > this.Data[4];
            }
        }
        public bool IsGreen
        {
            get
            {
                return this.Data[1] <= this.Data[4];
            }
        }
        public double Vol
        {
            get
            {
                return this.Data.Last();
            }
        }
        public double[] Data { get; set; }
       
      
        public double Ema12Value { get; set; }
        public double Ema26Value { get; set; }
        public double DifValue { get; set; }
        public double DeaValue { get; set; }
        public double MacdValue { get; set; }
        
    }
}
