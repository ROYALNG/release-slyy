using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace GHCore.Serialization
{
    public class JSONFormatter
    {
        public static string Serialize(object resource)
        {
            var textWriter = new StringWriter();
            var jsonSerializer = new Newtonsoft.Json.JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            jsonSerializer.Converters.Add(new IsoDateTimeConverter());

            jsonSerializer.Serialize(textWriter, resource);
            return textWriter.ToString();
        }
        public static T Deserialize<T>(string content)
        {
            if (string.IsNullOrEmpty(content))
                return default(T);

            var serializer = new JsonSerializer();
            var jsonTextReader = new JsonTextReader(new StringReader(content));

            //var method = typeof(JsonSerializer).GetMethods().First(m => m.Name == "Deserialize" && m.IsGenericMethod);
            //var generic = method.MakeGenericMethod(typeof(T));

            //return (T)generic.Invoke(serializer, new object[] { jsonTextReader });
            return serializer.Deserialize<T>(jsonTextReader);
        }
    }
}
