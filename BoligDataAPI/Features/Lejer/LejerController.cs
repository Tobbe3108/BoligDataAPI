using BoligDataAPI.Features.Header;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejer;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class LejerController : ControllerBase
{
  private readonly LejerService.Factory _lejerServiceFactory;

  public LejerController(LejerService.Factory lejerServiceFactory)
  {
    _lejerServiceFactory = lejerServiceFactory;
  }

  [HttpGet("/Lejer/{id:guid}")]
  public ActionResult<Response?> Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Lejemaal/{id:guid}/Lejer")]
  public ActionResult<IEnumerable<Response>> GetByLejemaalId(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).GetByLejemaalId(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }

  [HttpPost("/Lejer")]
  public ActionResult<Response> Create([FromBody] CreateRequest data)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Create(data.Adapt<Database.Lejer>() with { ApiKey = apiKey });
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpPut("/Lejer/{id:guid}")]
  public ActionResult<Response> Update(Guid id, [FromBody] UpdateRequest data)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Update(data.Adapt<Database.Lejer>() with { Id = id, ApiKey = apiKey });
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpDelete("/Lejer/{id:guid}")]
  public ActionResult Delete(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Delete(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok();
  }
}