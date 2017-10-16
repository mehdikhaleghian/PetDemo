using PetDemo.Model;
using PetDemo.Web.Models;

namespace PetDemo.Web.ModelMappers.Inerfaces
{
    public interface ICatViewModelMapper
    {
        CatViewModel Map(Person[] people);
    }
}
