using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rozklad.Parser;
using Rozklad.Parser.Entities;

namespace Rozklad.API.Controllers
{
    public class GroupController : ControllerBase
    {
        private readonly FetchGroup _fetchGroup;
        
        public GroupController(FetchGroup fetchGroup)
        {
            _fetchGroup = fetchGroup;
        }

        [HttpGet("groups")]
        public async Task<ActionResult<IReadOnlyCollection<ApiXmlResponse<string>>>> GetGroups()
        {
            //var groups = await _serviceRepository.GetActualStudyGroups(name);

            var groups = _fetchGroup.LoadData("");
            
          //  groups = groups.Take(Settings.DefaultPageSize).ToImmutableList();
            
            return Ok(groups);
        }

    }
}