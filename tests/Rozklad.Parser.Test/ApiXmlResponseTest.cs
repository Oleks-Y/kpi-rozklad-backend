using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Rozklad.Parser.Entities;
using Xunit;

namespace Rozklad.Parser.Test
{
    public class ApiXmlResponseTest
    {
        [Fact]
        public void CanBeParsedFromXml()
        {
            // Arrange 
            var xml = File.ReadAllText("ApiResponse.xml");
            var json = File.ReadAllText("JsonGroupData.json");
            // Act 
            
           var apiResponcse = new ApiXmlResponse<GroupTimetable>(xml);
           // Assert 
           Assert.True(apiResponcse.Data.InteropScheduleList[0].ScheduleItems.Count() > 1);
        }
    }
}