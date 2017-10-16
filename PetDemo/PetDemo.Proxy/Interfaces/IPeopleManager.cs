using System.Threading.Tasks;
using PetDemo.Model;

namespace PetDemo.Proxy.Interfaces
{
    public interface IPeopleManager
    {
        Task<Person[]> GetPeopleAsync();
        Person[] GetPeople();
    }
}
