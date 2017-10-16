using System.Net.Http;
using System.Threading.Tasks;
using PetDemo.Proxy.Interfaces;

namespace PetDemo.Proxy
{
    public class HttpHandler : IHttpHandler
    {
        private readonly HttpClient _client;
        public HttpHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}