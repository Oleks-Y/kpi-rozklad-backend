using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using RichardSzalay.MockHttp;
using Rozklad.Parser;
using Rozklad.Parser.Entities;
using Xunit;

namespace Rozklad.Parser.Test
{
    public class RozkladAPITest
    {
        
        [Fact]
        public async Task GetGroupFromApi()
        {
            // Arrange
            //
            var data = File.ReadAllText("ApiResponse.xml");
            var mockClient = new Mock<IRestClient>();
            var mockResponse = new Mock<IRestResponse<ApiXmlResponse<GroupTimetable>>>();
            var mockRequest = new Mock<IRestRequest>();
            mockResponse.Setup(r => r.StatusCode).Returns(HttpStatusCode.OK);
            mockResponse.Setup(r => r.Data).Returns(new ApiXmlResponse<GroupTimetable>(data));
            // TODO replace any type with exact query string
            mockClient.Setup(client => client.Execute<ApiXmlResponse<GroupTimetable>>(It.IsAny<IRestRequest>())).Returns(
                mockResponse.Object
            );
            var fetchGroup = new FetchGroup(mockClient.Object, "aaaa");
            // Act 
            var groupData = fetchGroup.LoadData("AA-11");
            // Assert 
            Assert.NotNull(groupData);
        }
        
       
    }
}