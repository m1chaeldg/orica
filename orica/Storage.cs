using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace orica
{
    public sealed class Storage : IStorage
    {
        public Document Load(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
                return new Document();

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();

            try
            {
                using (var sr = fileInfo.OpenText())
                using (var reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<Document>(reader);
                }
            }
            catch
            {
                return new Document();
            }
        }
    }


}