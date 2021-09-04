using System.Collections;
using System.Collections.Generic;
using RestSharp;
using rozklad_backend.src.Rozklad.Parser.Entities;

namespace rozklad_backend.src.Rozklad.Parser
{
    internal abstract class FetchEntity<T> : IFetchData<T>
    {
        protected readonly string _password; 
        private readonly RestClient _client;
        
        public FetchEntity(string baseUrl, string password)
        {
            _password = password;
            _client = new RestClient(baseUrl);
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
            var res = _client.Get<ApiXmlResponse<T>>(request);
            // TODO add serialization 
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


    internal class FetchGroup : FetchEntity<GroupTimetable>
    {
        public FetchGroup(string password) : base( "https://reg.kpi.ua/NP.Dev/WebService.asmx", password)
        {
        }
        protected override string ConstructQuery(string id)
        {
            var queryUrl = "/InteropGetGroupSchedulesAsJson?groupName=" + id + "&password=" + _password;
            return queryUrl;
        }
    }
}