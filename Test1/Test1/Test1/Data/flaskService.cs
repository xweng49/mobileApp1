using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;

namespace Test1.Data
{
    public class flaskService : IService
    {
        HttpClient client;
        public flaskService()
        {
            client = new HttpClient();
        }
        public async Task<string> InvokeGet(string request)
        {
            string url = Url.Combine(Constants.IPaddress, Uri.EscapeUriString(request));
            return await client.GetStringAsync(url);
        }



        public async Task<string> InvokePost(string request, string jsonObject)
        {
            string url = Url.Combine(Constants.IPaddress, Uri.EscapeUriString(request));
            HttpResponseMessage results = await client.PostAsync(url, new StringContent(jsonObject, Encoding.UTF8, "application/json"));

            return await results.Content.ReadAsStringAsync();
        }

        public Task<string> InvokeHead(string url, string request)
        {
            throw new NotImplementedException();
        }

    }
}
