using BoligDataAPI.Features.Header;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejemaal;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class LejemaalController : ControllerBase
{
  private readonly LejemaalService.Factory _lejemaalServiceFactory;

  public LejemaalController(LejemaalService.Factory lejemaalServiceFactory)
  {
    _lejemaalServiceFactory = lejemaalServiceFactory;
  }

  [HttpGet("/Lejemaal/{id:guid}")]
  public ActionResult<Response?> Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejemaalServiceFactory(apiKey).GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Ejendom/{id:guid}/Lejemaal")]
  public ActionResult<IEnumerable<Response>> GetByEjendomId(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejemaalServiceFactory(apiKey).GetByEjendomId(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }
}