using BoligDataAPI.Features.Database;

namespace BoligDataAPI.Features.Data;

public class DataFaker : IDataFaker
{
  private readonly DataContext _context;

  public DataFaker(DataContext context)
  {
    _context = context;
    RimuTec.Faker.Config.Locale = "da-DK";
  }

  public void GenerateData(DataFakerConfiguration dataFakerConfiguration,
    IEnumerable<string> apiKeys)
  {
    foreach (var apiKey in apiKeys)
    {
      for (var i = 0; i < dataFakerConfiguration.NrOfEjendomme; i++)
      {
        var ejendom = new Ejendom.Ejendom(RimuTec.Faker.Address.StreetName(),
          RimuTec.Faker.Address.BuildingNumber(),
          RimuTec.Faker.Address.Postcode(),
          RimuTec.Faker.Address.City(),
          RimuTec.Faker.Address.State(),
          RimuTec.Faker.Address.CountryCode());

        for (var j = 0; j < GetRandomFromRange(dataFakerConfiguration.RangeOfLejemaalPrEjendom); j++)
        {
          var lejemaal = new Lejemaal.Lejemaal(ejendom.Id,
            ejendom.StreetName,
            ejendom.BuildingNumber,
            RimuTec.Faker.Address.SecondaryAddress(),
            ejendom.Postcode,
            ejendom.City,
            ejendom.State,
            ejendom.CountryCode,
            Random.Shared.Next(100) < 10 /*10% chance of being bookable*/);

          var lejerePrLejemaal = GetRandomFromRange(dataFakerConfiguration.RangeOfLejerePrLejemaal);
          var lastDate = RimuTec.Faker.Date.Backward(3650);
          for (var h = 0; h < lejerePrLejemaal; h++)
          {
            var nextDate = RimuTec.Faker.Date.Between(lastDate, lastDate.AddYears(2));
            
            var lejer = new Lejer.Lejer
            {
              LejemaalId = lejemaal.Id,
              FirstName = RimuTec.Faker.Name.FirstName(),
              MiddleName = RimuTec.Faker.Name.MiddleName(),
              LastName = RimuTec.Faker.Name.LastName(),
              Email = RimuTec.Faker.Internet.Email(),
              PhoneNumber = RimuTec.Faker.PhoneNumber.CellPhone(),
              MoveOutDate = lastDate,
              MoveInDate = nextDate
            };
            lastDate = nextDate;

            lejer.ApiKey = apiKey;
            _context.Lejere.Add(lejer);
          }

          lejemaal.ApiKey = apiKey;
          _context.Lejemaal.Add(lejemaal);
        }

        ejendom.ApiKey = apiKey;
        _context.Ejendomme.Add(ejendom);
      }
    }

    _context.SaveChanges();
  }

  private static int GetRandomFromRange(string rangeString)
  {
    var range = rangeString.Split(',');
    var value = Random.Shared.Next(int.Parse(range.First()), int.Parse(range.Last()) + 1);
    return value;
  }
}