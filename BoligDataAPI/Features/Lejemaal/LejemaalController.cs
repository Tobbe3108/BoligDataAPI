using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejemaal;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class LejemaalController : ControllerBase
{
  private readonly LejemaalService _lejemaalService;

  public LejemaalController(LejemaalService lejemaalService)
  {
    _lejemaalService = lejemaalService;
  }

  [HttpGet("/Lejemaal/{id:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<Lejemaal?> Get(Guid id)
  {
    var result = _lejemaalService.GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }

  [HttpGet("/Ejendom/{id:guid}/Lejemaal")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<IEnumerable<Lejemaal>> GetByEjendomId(Guid id)
  {
    var result = _lejemaalService.GetByEjendomId(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }
}