using System.Net.Http;
using System.Threading.Tasks;

namespace PetDemo.Proxy.Interfaces
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
