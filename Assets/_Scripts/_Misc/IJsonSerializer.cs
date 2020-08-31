namespace PopovMaks.RAV3_Test
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string json);
    }
}