using System.Xml.Serialization;
using System.Text.Json;

namespace Rozklad.Parser.Entities
{
    public record ApiXmlResponse<T>
    {
        [XmlElement("string")]
        public string String { get; set; }
        [XmlIgnore]
        public T Data { get; set; }
        /// <summary>
        /// // TODO if parameters can`t automatically pass to consstructor rename to method like 'init' and call from code
        /// /// </summary>
        /// <param name="data"></param>
        public ApiXmlResponse()
        {
            Data = JsonSerializer.Deserialize<T>(String);
        }
    }
}