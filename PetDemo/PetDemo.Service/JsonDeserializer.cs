using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using PetDemo.Service.Interfaces;

namespace PetDemo.Service
{
    public class JsonDeserializer<T> : IJsonDeserializer<T> where T : class
    {
        public T Deserialize(string input)
        {
            if (!ValidateInputSchema(input))
                throw new JsonException("The given input is not valid");
            return JsonConvert.DeserializeObject<T>(input);
        }

        private bool ValidateInputSchema(string input)
        {
            var type = typeof(T);
            var schemaGenerator =
                new JSchemaGenerator
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            schemaGenerator.GenerationProviders.Add(new StringEnumGenerationProvider());
            var schema = schemaGenerator.Generate(type);
            if (type.IsArray)
            {
                var jArray = JArray.Parse(input);
                return jArray.IsValid(schema);
            }
            var jObject = JObject.Parse(input);
            return jObject.IsValid(schema);
        }
    }
}
