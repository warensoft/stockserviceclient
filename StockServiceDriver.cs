using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using Warensoft.EntLib.StockServiceClient.Models;
using static Warensoft.EntLib.StockServiceClient.Extensions;
namespace Warensoft.EntLib.StockServiceClient
{
    /// <summary>
    /// Warensoft Stock Service 客户端驱动
    /// </summary>
    public class StockServiceDriver
    {

        static string SERVERURL = "http://stockapi.warensoft.iego.net/Api/";
        /// <summary>
        /// API Key
        /// </summary>
        public string ApiKey { get; private set; }
        /// <summary>
        /// Secret Key
        /// </summary>
        public string SecretKey { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiKey">API Key</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="url">远程服务地址</param>
        public StockServiceDriver(string apiKey, string secretKey, string url = "http://stockapi.warensoft.iego.net/Api/")
        {
            SERVERURL = url;
            this.ApiKey = apiKey;
            this.SecretKey = secretKey;
        }
        /// <summary>
        /// 将请求加入请求队列
        /// </summary>
        /// <param name="method">请求地址</param>
        /// <param name="paramters">Post参数</param>
        /// <returns></returns>
        public async Task<string> SendToServer(string method, Dictionary<string, object> paramters = null)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("api_key", this.ApiKey);
            if (paramters != null)
            {
                foreach (var item in paramters)
                {
                    postData.Add(item.Key, item.Value.ToString());
                }
            }
            StringBuilder sbParam = new StringBuilder();

            foreach (var item in postData.OrderBy(a => a.Key))
            {
                sbParam.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            var sign = GetHMACString(sbParam.ToString(), this.SecretKey, Encoding.UTF8, new HMACSHA1());
            postData.Add("sign", sign);
            var url = $"{SERVERURL}{method}";
            return await this.Request(url, postData);
        }
        /// <summary>
        /// 将Ticker数据交化为Kline
        /// </summary>
        /// <param name="tickers">Tickers</param>
        /// <param name="span">时间间隔，单位为秒，默认1分钟</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<Kline>>> GetKline(List<Ticker> tickers, int span = 60)
        {
            var data = tickers.ToJsonString().ToBase64String();

            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Tickers", data);
            postData.Add("Span", span);
            var result = await this.SendToServer("Kline", postData);
            var klines = result.FromJsonString<ResultInfoC<List<Kline>>>();
            return klines;
        }
        /// <summary>
        /// 获取K线的MACD值
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="emaShort">短周期均线次数，默认12</param>
        /// <param name="emaLong">长周期均线次数，默认26</param>
        /// <param name="emaSecond">二次均线次数，默认9</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<Kline>>> GetMACD(List<Kline> kline, int emaShort = 12, int emaLong = 26, int emaSecond = 9)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("EMAShort", emaShort);
            postData.Add("EMALong", emaLong);
            postData.Add("EMASecond", emaSecond);
            var result = await this.SendToServer("MACD", postData);
            var klines = result.FromJsonString<ResultInfoC<List<Kline>>>();
            return klines;
        }
        /// <summary>
        /// 获取K线的EMA均线
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="period">周期</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetEMA(List<Kline> kline, int period = 12)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("Period", period);

            var result = await this.SendToServer("EMA", postData);
            var ema = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return ema;
        }
        /// <summary>
        /// 获取K线的SMA均线
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="period">周期</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetSMA(List<Kline> kline, int period = 12)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("Period", period);

            var result = await this.SendToServer("SMA", postData);
            var ema = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return ema;
        }
        /// <summary>
        /// 获取K线的SAR线
        /// </summary>
        /// <param name="kline">K线</param> 
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetSAR(List<Kline> kline)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            var result = await this.SendToServer("SAR", postData);
            var sar = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return sar;
        }
        /// <summary>
        /// 获取K线的RSI线
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="period">周期</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetRSI(List<Kline> kline, int period = 12)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("Period", period);

            var result = await this.SendToServer("RSI", postData);
            var ema = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return ema;
        }
        /// <summary>
        /// 获取K线的威廉指标（Williams %R）线
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="period">周期</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetWR(List<Kline> kline, int period = 12)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("Period", period);

            var result = await this.SendToServer("WR", postData);
            var ema = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return ema;
        }
        /// <summary>
        /// 获取K线的ATR(平均真实波幅)线
        /// </summary>
        /// <param name="kline">K线</param>
        /// <param name="period">周期</param>
        /// <returns></returns>
        public async Task<ResultInfoC<List<TimeValuePair>>> GetATR(List<Kline> kline, int period = 12)
        {
            var data = kline.ToJsonString().ToBase64String();
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("Kline", data);
            postData.Add("Period", period);

            var result = await this.SendToServer("ATR", postData);
            var ema = result.FromJsonString<ResultInfoC<List<TimeValuePair>>>();
            return ema;
        }
    }
}
