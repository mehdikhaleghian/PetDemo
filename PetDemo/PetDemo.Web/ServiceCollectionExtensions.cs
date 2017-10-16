using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace PetDemo.Web
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClient(this IServiceCollection services, string baseAddress)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            services.AddSingleton(client);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            services.AddSingleton(client);
        }
    }
}
