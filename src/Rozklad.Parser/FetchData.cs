using System;
using RestSharp;
using Rozklad.Parser.Entities;

namespace Rozklad.Parser
{
    public abstract class FetchEntity<T> : IFetchData<T>
    {
        protected readonly string _password; 
        private readonly RestClient _client;
        protected readonly string baseUrl;
        
        public FetchEntity(string baseUrl, string password)
        {
            _password = password;
            _client = new RestClient(baseUrl);
            this.baseUrl = baseUrl;
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
            var response = res.Content;
            throw new NotImplementedException();
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
        public FetchGroup(string password) : base( "http://reg.kpi.ua/NP.Dev/WebService.asmx", password)
        {
        }
        protected override string ConstructQuery(string id)
        {
            var queryUrl = this.baseUrl + "/InteropGetGroupSchedulesAsJson?groupName=" + id + "&password=" + _password;
            return queryUrl;
        }
    }
}