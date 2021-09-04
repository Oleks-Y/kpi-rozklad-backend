using System.Xml.Serialization;
using System.Text.Json;

namespace Rozklad.Parser.Entities
{
    public record ApiXmlResponse<T>
    {
        [XmlElement("string")]
        public string String { get; set; }
        
        public T Data { get; set; }
        /// <summary>
        /// // TODO if parameters can`t automatically pass to consstructor rename to method like 'init' and call from code
        /// /// </summary>
        /// <param name="data"></param>
        public ApiXmlResponse(string data)
        {
            Data = JsonSerializer.Deserialize<T>(data);
        }
    }
}