using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _http;
        public HttpService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Result<T>> GetData<T>(string url)
        {
            var result = await _http.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                if (result.Content.ReadAsStringAsync() != null)
                {
                    var deserialize = await Deserialize<T>(result, serializeOptions());
                    return new Result<T>(deserialize, true, result);
                }else
                {
                    return new Result<T>(default, false, result);
                }
                
            }else
            {
                return new Result<T>(default , false , result);
            }
        }

        public async Task<T> Deserialize<T>(HttpResponseMessage httpResp , JsonSerializerOptions option)
        {
            var responseBody = await httpResp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody , option);
        }

        public async Task<Result<TResponse>> PostAsync<T, TResponse>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, serializeOptions());
                return new Result<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new Result<TResponse>(default, false, response);
            }
        }

        private JsonSerializerOptions serializeOptions()
        {
            return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
    }
     
}
