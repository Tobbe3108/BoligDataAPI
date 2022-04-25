using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Ejendom;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EjendomController : ControllerBase
{
  private readonly EjendomService _ejendomService;

  public EjendomController(EjendomService ejendomService)
  {
    _ejendomService = ejendomService;
  }

  [HttpGet("/Ejendom/{id:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<Ejendom?> Get(Guid id)
  {
    var result = _ejendomService.GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }

  [HttpGet("/Ejendom")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<IEnumerable<Ejendom>> List()
  {
    var result = _ejendomService.GetAll();
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }
}