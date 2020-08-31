using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    public class UnitySerializer : IJsonSerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public T Deserialize<T>(string json)
        {
            return !string.IsNullOrWhiteSpace(json) ? JsonUtility.FromJson<T>(json) : default;
        }
    }
}