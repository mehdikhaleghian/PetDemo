using System.Collections.Generic;
using PetDemo.Model.Enums;

namespace PetDemo.Model
{
    public class Person
    {
        public Person()
        {
            Pets = new List<Pet>();
        }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public ushort Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
    }
}
