using Newtonsoft.Json.Linq;
using System.IO;

namespace HiSeer.src
{
    public static class JsonHandler
    {
        public static JObject GetJsonObject(string path)
        {
            string json = File.ReadAllText(path);
            var obj = JObject.Parse(json);

            return obj;
        }
    }
}
