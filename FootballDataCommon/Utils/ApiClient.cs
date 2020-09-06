using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballDataCommon.Utils
{
    public class ApiClient
    {
        public static async Task<T> get<T>(string url){

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-Auth-Token", "cef3de66b69242d88db42d1b0e0a5d8a");

            var streamTask = client.GetStreamAsync(url);
            var data = await JsonSerializer.DeserializeAsync<T>(await streamTask);
            return data;
        }
    }
}
