using System.Xml.Serialization;
using System.Text.Json;
using System.Xml;

namespace Rozklad.Parser.Entities
{
    public record ApiXmlResponse<T>
    {
        public T Data { get; }

        public ApiXmlResponse(string xml)
        {
            
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var json = xmlDoc.GetElementsByTagName("string")[0].InnerXml;
            var stringFormatted = json.Replace("'", "\"");
            Data = JsonSerializer.Deserialize<T>(stringFormatted);
        }
    }
}