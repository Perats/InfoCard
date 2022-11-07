using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoCard.Client
{
    public class HttpHelper
    {

        public static async Task<T> Get<T>(string url)
        {
            T responseObj;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync(url).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        responseObj = JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        throw new Exception("Unexpected ressult");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseObj;
        }

        public static async Task<TResult> Send<TArg, TResult>(HttpMethod method, string url, TArg arg)
        {
            TResult responseObj;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5000/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = new HttpResponseMessage();
                    var request = new HttpRequestMessage(method, url)
                    {
                        Content = new StringContent(JsonSerializer.Serialize(arg), Encoding.UTF8, "application/json"),
                    };
                    response = await client.SendAsync(request).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(result))
                        {
                            responseObj = default;
                        }
                        else
                        {
                            responseObj = JsonSerializer.Deserialize<TResult>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        response.Dispose();
                    }
                    else
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        throw new Exception("Unexpected ressult");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseObj;
        }
    }
}
