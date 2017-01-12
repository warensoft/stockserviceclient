using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Net.Http;

namespace Warensoft.EntLib.StockServiceClient
{
    public static class Extensions
    {
        public static string ToJsonString(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T FromJsonString<T>(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return default(T);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
        }
        public static string ToBase64String(this string s)
        {
            var buffer = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(buffer);
        }
        /// <summary>
        /// 将JSON字符串转化为JTOKEN对象
        /// </summary>
        /// <param name="s">JSON字符串</param>
        /// <returns></returns>
        public static List<JToken> FromJsonStringJToken(this string s)
        {
            List<JToken> json = JValue.Parse(s).ToList();
            return json;
        }
        private static char[] HEX_DIGITS = new char[]{'0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        public static string GetHMACString(string str, string privateKey, Encoding encoding, HMAC hmac)
        {
            if (str == null || str.Trim().Length == 0)
            {
                return "";
            }
            hmac.Key = encoding.GetBytes(privateKey);
            var bytes = hmac.ComputeHash(encoding.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(HEX_DIGITS[(bytes[i] & 0xf0) >> 4] + ""
                        + HEX_DIGITS[bytes[i] & 0xf]);
            }
            return sb.ToString();
        }

        static HttpRequestManager hrmgr;
        /// <summary>
        /// 获取请求处理器
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static HttpRequestManager GetRequestProvider(this StockServiceDriver driver,int interval)
        {
            if (hrmgr == null)
            {
                hrmgr = new HttpRequestManager();
                hrmgr.RequestInterval = interval;
                hrmgr.Start();
            }
            return hrmgr;
        }
        static HttpClient httpClient = new HttpClient();
        public static async Task<string> Request(this StockServiceDriver driver, string url, Dictionary<string, string> postData = null)
        {
            if (postData == null)
            {
                var s =await httpClient.GetStringAsync(url);
                return s;
            }
            else
            {
                StringBuilder sbParam = new StringBuilder();

                StringContent content1 = new StringContent(sbParam.ToString());
                MultipartFormDataContent content2 = new MultipartFormDataContent();
                foreach (var item in postData)
                {
                    //sbParam.AppendFormat("{0}={1}&", item.Key, item.Value);
                    content2.Add(new StringContent(item.Value), item.Key);
                }
                //sbParam.Remove(sbParam.Length - 1, 1);
                //FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                var response = httpClient.PostAsync(url, content2).Result;
                var s =await response.Content.ReadAsStringAsync();
                return s;
            }
            //return await driver.GetRequestProvider(150).Request(url, postData);
        }
    }
}
