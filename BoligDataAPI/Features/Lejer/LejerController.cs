using BoligDataAPI.Features.Header;
using BoligDataAPI.Features.Results;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Features.Lejer;

[ApiController]
[Route("[controller]")]
public class LejerController : ControllerBase
{
  private readonly ILejerService.Factory _lejerServiceFactory;

  public LejerController(ILejerService.Factory lejerServiceFactory)
  {
    _lejerServiceFactory = lejerServiceFactory;
  }

  [HttpGet("/Lejer/{id:guid}")]
  [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Get(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).GetById(id);

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpGet("/Lejemaal/{id:guid}/Lejer")]
  [ProducesResponseType(typeof(IEnumerable<Response>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult GetByLejemaalId(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).GetByLejemaalId(id);

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<IEnumerable<Response>>());
  }

  [HttpPost("/Lejer")]
  [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Create([FromBody] CreateRequest data)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Create(data.Adapt<Lejer>() with { ApiKey = apiKey });

    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpPut("/Lejer/{id:guid}")]
  [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Update(Guid id, [FromBody] UpdateRequest data)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Update(data.Adapt<Lejer>() with { Id = id, ApiKey = apiKey });
    
    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : Ok(result.Value.Adapt<Response>());
  }

  [HttpDelete("/Lejer/{id:guid}")]
  [ProducesResponseType( StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(IEnumerable<IReason>), StatusCodes.Status409Conflict)]
  public IActionResult Delete(Guid id)
  {
    var apiKey = Request.Headers.ExtractApiKey();
    var result = _lejerServiceFactory(apiKey).Delete(id);
    
    return result.IsFailed
      ? result.HasError<NotFoundError>()
        ? NotFound(result.Reasons)
        : Conflict(result.Reasons)
      : NoContent();
  }
}