using System.Threading.Tasks;
using PetDemo.Model;

namespace PetDemo.Service.Interfaces
{
    public interface IPeopleService
    {
        Task<Person[]> GetPeopleAsync();
    }
}
