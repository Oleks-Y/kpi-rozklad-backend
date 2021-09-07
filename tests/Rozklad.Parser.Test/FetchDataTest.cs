using System;
using System.IO;
using System.Net.Http;
using RichardSzalay.MockHttp;
using Rozklad.Parser;
using Xunit;

namespace Rozklad.Parser.Test
{
    public class RozkladAPITest
    {
        [Fact]
        public async void GetJsonFromApi()
        {
            //Arrange
            var fetchGroup = new FetchGroup("YaroslavK_E_D30E0C9E8392");
            var mockHttp = new MockHttpMessageHandler();
            var data = File.ReadAllText("/Users/alex/proj/kpi/rozklad-backend/tests/Rozklad.Parser.Test/ApiResponse.xml");
            mockHttp.When("https://reg.kpi.ua/NP.Dev/WebService.asmx/InteropGetGroupSchedulesAsJson?groupName=%D0%A5%D0%95-11&password=YaroslavK_E_D30E0C9E8392")
                .Respond("text/xml", data);
            var fetchData = new FetchGroup("YaroslavK_E_D30E0C9E8392");
            // Act
            var groupData = fetchData.LoadData("ХЕ-11");
            // Assert 
            Assert.NotNull(groupData);

        }
        
    }
}