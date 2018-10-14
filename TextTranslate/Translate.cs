using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TextTranslate
{
    public class Translate
    {
        public async Task<string> TranslateText(string uriRequest, string textToTranslate, string key)
        {
            System.Object[] body = new System.Object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (HttpClient httpClient = new HttpClient())
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.Method = HttpMethod.Post;
                httpRequestMessage.RequestUri = new Uri(uriRequest);
                httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                httpRequestMessage.Headers.Add("Ocp-Apim-Subscription-Key", key);
                var responseAync = await httpClient.SendAsync(httpRequestMessage);
                var responseBody = await responseAync.Content.ReadAsStringAsync();
                dynamic textResult = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);

                return textResult;
            }
        }
    }
}
