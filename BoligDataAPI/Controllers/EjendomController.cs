using Microsoft.AspNetCore.Mvc;

namespace BoligDataAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  [HttpGet("{id:guid}")]
  public string Get(Guid id)
  {
    return RimuTec.Faker.Address.FullAddress();
  }
}