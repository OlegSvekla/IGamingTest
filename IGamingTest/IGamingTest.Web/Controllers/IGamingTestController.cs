using Asp.Versioning;
using IGamingTest.BL.Services;
using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Core.Models;
using IGamingTest.Web.Mappers.ToQuery;
using IGamingTest.Web.Rqs;
using IGamingTest.Web.Rss;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IGamingTest.Web.Controllers
{
    [Route("api/v{version:apiVersion}/igaming")]
    [ApiController]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(Status400BadRequest, Type = typeof(FieldErrorRs[]))]
    [ProducesResponseType(Status500InternalServerError, Type = typeof(MessageErrorRs))]
    [ProducesResponseType(Status429TooManyRequests, Type = typeof(void))]
    public class IGamingTestController(
        IMapper<MeteoriteFilterRq, MeteoriteFilterQuery> meteoriteFilterRqToQueryMapper,
        IMeteoriteService meteoriteDataClient)
        : ControllerBase
    {
        [HttpGet("meteorites")]
        [ProducesResponseType(Status200OK, Type = typeof(IReadOnlyCollection<MeteoriteGroupedDto>))]
        public async Task<IActionResult> GetEventsAsync(
            [FromBody] MeteoriteFilterRq rq,
            CancellationToken ct)
        {
            var query = meteoriteFilterRqToQueryMapper.Map(rq);

            var queryRs = await meteoriteDataClient.GetFilteredGroupedMeteoritesAsync(query, ct);

            return Ok(queryRs);
        }
    }
}
