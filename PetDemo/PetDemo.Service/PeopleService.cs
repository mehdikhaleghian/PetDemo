using System;
using System.Net;
using System.Threading.Tasks;
using PetDemo.Model;
using PetDemo.Proxy.Interfaces;
using PetDemo.Service.Interfaces;

namespace PetDemo.Service
{
    public class PeopleService : IPeopleService
    {
        private readonly IHttpHandler _httpHandler;
        private readonly IJsonDeserializer<Person[]> _deserializer;

        public PeopleService(IHttpHandler httpHandler, IJsonDeserializer<Person[]> deserializer)
        {
            _httpHandler = httpHandler;
            _deserializer = deserializer;
        }
        public async Task<Person[]> GetPeopleAsync()
        {
            var responseMessage = await _httpHandler.GetAsync("people");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return _deserializer.Deserialize(jsonData);
            }

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.RequestTimeout:
                case HttpStatusCode.GatewayTimeout:
                    throw new TimeoutException("Getting people has timed out");
                case HttpStatusCode.NotFound:
                    throw new InvalidOperationException("The given resource does not exist");
                default:
                    throw new ApplicationException("Something went wrong");
            }
        }

        public Person[] GetPeople()
        {
            return GetPeopleAsync().Result;
        }
    }
}
