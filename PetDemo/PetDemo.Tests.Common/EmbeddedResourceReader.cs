using System.IO;
using System.Reflection;

namespace PetDemo.Tests.Common
{
    public static class EmbeddedResourceReader
    {
        public static string ReadAsJsonString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
