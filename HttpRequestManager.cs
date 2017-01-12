using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Warensoft.EntLib.StockServiceClient
{
    public class HttpRequestManager
    {
        HttpClient httpClient = new HttpClient();
        public int RequestInterval = 100;
        private BlockingCollection<Task<string>> requests = new BlockingCollection<Task<string>>(new ConcurrentQueue<Task<string>>());

        /// <summary>
        /// 异步处理Http请求队列
        /// </summary>
        /// <returns></returns>
        private async Task ProcessRequestAsync()
        {
            await Task.Run(async () =>
            {
                foreach (var item in requests.GetConsumingEnumerable())
                {
                    item.Start();
                    await Task.Delay(RequestInterval);
                }
            });
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            this.ProcessRequestAsync();
        }
        /// <summary>
        /// 将请求加入请求队列
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">Post参数</param>
        /// <returns></returns>
        public async Task<string> Request(string url, Dictionary<string, string> postData = null)
        {
            return await Task.Run<string>(() =>
            {
                Task<string> task = new Task<string>(() =>
                {
                    if (postData == null)
                    {
                        var s = httpClient.GetStringAsync(url).Result;
                        return s;
                    }
                    else
                    {
                        StringBuilder sbParam = new StringBuilder();
                        
                        StringContent content1 = new StringContent(sbParam.ToString ());
                        MultipartFormDataContent content2 = new MultipartFormDataContent();
                        foreach (var item in postData)
                        {
                            //sbParam.AppendFormat("{0}={1}&", item.Key, item.Value);
                            content2.Add(new StringContent(item.Value), item.Key);
                        }
                        //sbParam.Remove(sbParam.Length - 1, 1);
                        //FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                        var response = httpClient.PostAsync(url, content2).Result;
                        var s = response.Content.ReadAsStringAsync().Result;
                        return s;
                    }

                });
                requests.Add(task);
                task.Wait();
                return task.Result;
            });
        }
    }
}
