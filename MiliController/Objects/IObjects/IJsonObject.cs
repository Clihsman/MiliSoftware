namespace MiliSoftware
{
    public interface IJsonObject
    {
        string ToJson();
        void FromJson(string json);
    }
}
