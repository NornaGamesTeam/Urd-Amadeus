
namespace Urd.Utils
{
    public static class StringExtension
    {
        public static string ToJson(this object toConvertToJson)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(toConvertToJson);
            }
            catch
            {
                UnityEngine.Debug.LogWarning("Something happened when try to convert to json");
                return toConvertToJson.ToString();
            }
        }
    }
}