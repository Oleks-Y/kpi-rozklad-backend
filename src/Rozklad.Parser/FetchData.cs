using System;
using System.Net;
using RestSharp;
using RestSharp.Serialization.Xml;
using Rozklad.Parser.Entities;

namespace Rozklad.Parser
{
    public abstract class FetchEntity<T> : IFetchData<T>
    {
        protected readonly string _password;
        private readonly IRestClient _client;
        protected readonly string baseUrl;

        public FetchEntity(IRestClient client, string password)
        {
            _password = password;
            _client = client;
        }

        /// <summary>
        /// mapping of method and query params
        /// </summary>
        /// <param name="id">identifier of entity</param>
        /// <returns></returns>
        protected abstract string ConstructQuery(string id);

        public T LoadData(string id)
        {
            var query = this.ConstructQuery(id);
            var request = new RestRequest(query, DataFormat.Xml);
            var res = _client.Execute<ApiXmlResponse<T>>(request);
            if (res.StatusCode == HttpStatusCode.NotFound)
            {
                throw new GroupNotFoundException();
            }

            var response = res.Data;
            return response.Data;
        }
    }

    /// <summary>
    /// Load data from API 
    /// </summary>
    internal interface IFetchData<T>
    {
        public T LoadData(string id);
    }


    public class FetchGroup : FetchEntity<GroupTimetable>
    {
        public FetchGroup(IRestClient client,string password) : base(client, password)
        {
        }

        protected override string ConstructQuery(string id)
        {
            var queryUrl = this.baseUrl + "/InteropGetGroupSchedulesAsJson?groupName=" + id + "&password=" + _password;
            return queryUrl;
        }
    }

    public class GroupNotFoundException : Exception
    {
    }
}