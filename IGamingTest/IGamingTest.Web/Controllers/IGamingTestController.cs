using Asp.Versioning;
using IGamingTest.Contracts.Queries;
using IGamingTest.Core.Mappers;
using IGamingTest.Core.Models;
using IGamingTest.Core.Mq.Buses;
using IGamingTest.Web.Rqs;
using IGamingTest.Web.Rss;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IGamingTest.Web.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(Status400BadRequest, Type = typeof(FieldErrorRs[]))]
[ProducesResponseType(Status500InternalServerError, Type = typeof(MessageErrorRs))]
[ProducesResponseType(Status429TooManyRequests, Type = typeof(void))]
public class IGamingTestController(
    ILocalMessageBus localMessageBus,
    IMapper<GetMeteoriteFilterRq, GetMeteoriteFilterQuery> meteoriteFilterRqToQueryMapper,
    IMapper<IReadOnlyCollection<GetMeteoriteFilterQueryRs>, IReadOnlyCollection<GetMeteoriteFilterRs>> meteoriteFilterQueryRsToRsMapper)
    : ControllerBase
{
    [HttpPost("meteorites")]
    [ProducesResponseType(Status200OK, Type = typeof(IReadOnlyCollection<GetMeteoriteFilterRs>))]
    public async Task<IActionResult> GetMeteoritesAsync(
        [FromBody] GetMeteoriteFilterRq rq,
        CancellationToken ct)
    {
        var query = meteoriteFilterRqToQueryMapper.Map(rq);

        var queryRs = await localMessageBus.DispatchAsync(query, ct);

        var rs = meteoriteFilterQueryRsToRsMapper.Map(queryRs);

        return Ok(rs);
    }
}
