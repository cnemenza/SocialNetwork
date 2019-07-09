using Newtonsoft.Json;
using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers
{
    public class ApiHandler : IApiHandler
    {
        public async Task<T> GetAsync<T>(string uri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(ConstantsHelpers.Api.BASEURI) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.GetAsync(uri))
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseString);
                }
            }
        }

        public async Task<T2> PostAsync<T1, T2>(T1 data, string uri)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(ConstantsHelpers.Api.BASEURI) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var serializedEntity = JsonConvert.SerializeObject(data);
                var content = new StringContent(serializedEntity, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(uri, content))
                {
                    var serviceResponse = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode) throw new Exception(JsonConvert.DeserializeObject<ErrorModel>(serviceResponse).Message);
                    return JsonConvert.DeserializeObject<T2>(serviceResponse);
                }
            }
        }
    }
}