using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using BookStore.Models.Models.Configurations;
using Microsoft.Extensions.Options;

namespace BookStore.BL.DeliveryPurchaseConsumer
{
    public class HttpClientProvider
    {
        private HttpClient _client;
        private IOptions<HttpClientConfig> _httpClientConfig;

        public HttpClientProvider(IOptions<HttpClientConfig> httpClientConfig)
        {
            _httpClientConfig = httpClientConfig;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_httpClientConfig.Value.Connection);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public async Task<IEnumerable<string>> AddAditionalInfo()
        //{
        //    var response = await _client.GetAsync(_client.BaseAddress);

        //    response.EnsureSuccessStatusCode();

        //    var result = response.Content.ReadAsStringAsync().Result;

        //    var authorAddInfo = JsonConvert.DeserializeObject<IEnumerable<string>>(result);

        //    return authorAddInfo;
        //}

        public async Task<IEnumerable<AdditionalInfo>> AddAditionalInfo()
        {
            var response = await _client.GetAsync(_client.BaseAddress);

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync().Result;

            var authorAddInfo = JsonConvert.DeserializeObject<IEnumerable<AdditionalInfo>>(result);

            return authorAddInfo;
        }


    }
}
