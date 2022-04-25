using BoligDataAPI.Features.Lejer.Requests;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejer;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class LejerController : ControllerBase
{
  private readonly LejerService _lejerService;

  public LejerController(LejerService lejerService)
  {
    _lejerService = lejerService;
  }

  [HttpGet("/Lejer/{id:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<Lejer?> Get(Guid id)
  {
    var result = _lejerService.GetById(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }

  [HttpGet("/Lejemaal/{id:guid}/Lejer")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult<IEnumerable<Lejer>> GetByLejemaalId(Guid id)
  {
    var result = _lejerService.GetByLejemaalId(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }
  
  [HttpPost("/Lejer")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult Create([FromBody] CreateRequest data)
  {
    var result = _lejerService.Create(data.Adapt<Lejer>());
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }
  
  [HttpPut("/Lejer/{id:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult Update(Guid id, [FromBody] UpdateRequest data)
  {
    var result = _lejerService.Update(data.Adapt<Lejer>() with { Id = id });
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok(result.Value);
  }
  
  [HttpDelete("/Lejer/{id:guid}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public ActionResult Delete(Guid id)
  {
    var result = _lejerService.Delete(id);
    return result.IsFailed
      ? Conflict(result.ToString())
      : Ok();
  }
}