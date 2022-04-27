using BoligDataAPI.Features.Header;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Ejendom;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class EjendomController : ControllerBase
{
  private readonly IEjendomService.Factory _ejendomServiceFactory;

  public EjendomController(IEjendomService.Factory ejendomServiceFactory)
  {
    _ejendomServiceFactory = ejendomServiceFactory;
  }

  [HttpGet("/Ejendom/{id:guid}")]
  public ActionResult<Response?> Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _ejendomServiceFactory(apiKey).GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Ejendom")]
  public ActionResult<IEnumerable<Response>> List()
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _ejendomServiceFactory(apiKey).GetAll();
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }
}