using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetDemo.Model;
using PetDemo.Proxy.Interfaces;

namespace PetDemo.Proxy
{
    public class PeopleManager : IPeopleManager
    {
        private readonly IHttpHandler _httpHandler;

        public PeopleManager(IHttpHandler httpHandler)
        {
            _httpHandler = httpHandler;
        }
        public async Task<Person[]> GetPeopleAsync()
        {
            var responseMessage = await _httpHandler.GetAsync("people");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Person[]>(jsonData);
            }

            if (responseMessage.StatusCode == HttpStatusCode.RequestTimeout)
                throw new TimeoutException("Getting people has timed out");
            throw new ApplicationException("Something went wrong");
        }

        public Person[] GetPeople()
        {
            return GetPeopleAsync().Result;
        }
    }
}
