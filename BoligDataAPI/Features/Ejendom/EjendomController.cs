using BoligDataAPI.Features.Header;
using BoligDataAPI.Features.Results;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Ejendom;

[ApiController]
[Route("[controller]")]
public class EjendomController : ControllerBase
{
  private readonly IEjendomService.Factory _ejendomServiceFactory;

  public EjendomController(IEjendomService.Factory ejendomServiceFactory)
  {
    _ejendomServiceFactory = ejendomServiceFactory;
  }

  [HttpGet("/Ejendom/{id:guid}")]
  [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _ejendomServiceFactory(apiKey).GetById(id);

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Ejendom")]
  [ProducesResponseType(typeof(IEnumerable<Response>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult List()
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _ejendomServiceFactory(apiKey).GetAll();

    return result.IsFailed
      ? Conflict(result.Reasons)
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }
}