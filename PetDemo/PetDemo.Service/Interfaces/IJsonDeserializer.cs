namespace PetDemo.Service.Interfaces
{
    public interface IJsonDeserializer<out T> where T : class
    {
        T Deserialize(string input);
    }
}
