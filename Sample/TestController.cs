using Microsoft.AspNetCore.Mvc;
using Sample.Refit;
using Sample.Shared;

namespace Sample;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
  private readonly IBoligDataApi _api;

  public TestController(IBoligDataApi api)
  {
    _api = api;
  }
  
  [HttpGet]
  public async Task<OkObjectResult> Test()
  {
    var ejendomme = await _api.ListEjendomme() ?? throw new InvalidOperationException();

    var ejendomId = ejendomme.First().Id;

    var lejemaal = await _api.ListLejemaalOnEjendom(ejendomId) ?? throw new InvalidOperationException();

    var lejemaalId = lejemaal.First().Id;

    var lejerCreateRequest = new LejerCreateRequest(lejemaalId,
      "Tobias",
      "Gernhardt",
      "Lauritzen",
      "Tobbe3108@gmail.com",
      "56892356",
      DateTime.Today.AddYears(-2),
      null);

    var newLejer = await _api.CreateLejer(lejerCreateRequest) ?? throw new NullReferenceException();

    var lejer = await _api.GetLejer(newLejer.Id) ?? throw new InvalidOperationException();

    var lejerUpdateRequest = new LejerUpdateRequest("Tobias",
      "Gernhardt",
      "Lauritzen",
      "TobiasLauritzen@protonmail.com",
      "61708373",
      DateTime.Today.AddYears(-2),
      null);

    var updatedLejer = await _api.UpdateLejer(lejer.Id, lejerUpdateRequest) ?? throw new NullReferenceException();

    await _api.DeleteLejer(updatedLejer.Id);

    var lejere = await _api.ListLejereOnLejemaal(lejemaalId);

    return Ok(lejere);
  }
}