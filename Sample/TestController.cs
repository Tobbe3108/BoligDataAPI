using Microsoft.AspNetCore.Mvc;
using Sample.Refit;
using Sample.Shared;

namespace Sample;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
  [HttpGet]
  public async Task<OkObjectResult> Test([FromServices] IBoligDataApi api)
  {
    var ejendomme = await api.ListEjendomme() ?? throw new InvalidOperationException();

    var ejendomId = ejendomme.First().Id;

    var lejemaal = await api.ListLejemaalOnEjendom(ejendomId) ?? throw new InvalidOperationException();

    var lejemaalId = lejemaal.First().Id;

    var lejerCreateRequest = new LejerCreateRequest(lejemaalId,
      "Tobias",
      "Gernhardt",
      "Lauritzen",
      "Tobbe3108@gmail.com",
      "56892356",
      DateTime.Today.AddYears(-2),
      null);

    var newLejer = await api.CreateLejer(lejerCreateRequest) ?? throw new NullReferenceException();

    var lejer = await api.GetLejer(newLejer.Id) ?? throw new InvalidOperationException();

    var lejerUpdateRequest = new LejerUpdateRequest("Tobias",
      "Gernhardt",
      "Lauritzen",
      "TobiasLauritzen@protonmail.com",
      "61708373",
      DateTime.Today.AddYears(-2),
      null);

    var updatedLejer = await api.UpdateLejer(lejer.Id, lejerUpdateRequest) ?? throw new NullReferenceException();

    await api.DeleteLejer(updatedLejer.Id);

    var lejere = await api.ListLejereOnLejemaal(lejemaalId);

    return Ok(lejere);
  }
}