using Newtonsoft.Json;

namespace PopovMaks.RAV3_Test
{
    public class NewtonsoftSerializer : IJsonSerializer
    {
        public string Serialize<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}