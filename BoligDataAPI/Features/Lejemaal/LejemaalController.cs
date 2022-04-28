using BoligDataAPI.Features.Header;
using BoligDataAPI.Features.Results;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejemaal;

[ApiController]
[Route("[controller]")]
public class LejemaalController : ControllerBase
{
  private readonly ILejemaalService.Factory _lejemaalServiceFactory;

  public LejemaalController(ILejemaalService.Factory lejemaalServiceFactory)
  {
    _lejemaalServiceFactory = lejemaalServiceFactory;
  }

  [HttpGet("/Lejemaal/{id:guid}")]
  [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejemaalServiceFactory(apiKey).GetById(id);

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Ejendom/{id:guid}/Lejemaal")]
  [ProducesResponseType(typeof(IEnumerable<Response>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult GetByEjendomId(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejemaalServiceFactory(apiKey).GetByEjendomId(id);

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }
}