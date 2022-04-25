namespace BoligDataAPI.Features.Database;

public class DataFaker
{
  private readonly DataFakerConfiguration _dataFakerConfiguration;
  private readonly IEnumerable<string> _apiKeys;
  private readonly DataContext _context;

  public DataFaker(DataFakerConfiguration dataFakerConfiguration,
    IEnumerable<string> apiKeys, DataContext context)
  {
    _dataFakerConfiguration = dataFakerConfiguration;
    _apiKeys = apiKeys;
    _context = context;
    RimuTec.Faker.Config.Locale = "da-DK";
  }

  public void GenerateData()
  {
    foreach (var apiKey in _apiKeys)
    {
      for (var i = 0; i < _dataFakerConfiguration.NrOfEjendomme; i++)
      {
        var ejendom = new Ejendom(RimuTec.Faker.Address.StreetName(),
          RimuTec.Faker.Address.BuildingNumber(),
          RimuTec.Faker.Address.Postcode(),
          RimuTec.Faker.Address.City(),
          RimuTec.Faker.Address.State(),
          RimuTec.Faker.Address.CountryCode());

        for (var j = 0; j < GetRandomFromRange(_dataFakerConfiguration.RangeOfLejemaalPrEjendom); j++)
        {
          var lejemaal = new Lejemaal(ejendom.Id,
            ejendom.StreetName,
            ejendom.BuildingNumber,
            RimuTec.Faker.Address.SecondaryAddress(),
            ejendom.Postcode,
            ejendom.City,
            ejendom.State,
            ejendom.CountryCode,
            Random.Shared.Next(100) < 10 /*10% chance of being bookable*/);

          for (var h = 0; h < GetRandomFromRange(_dataFakerConfiguration.RangeOfLejerePrLejemaal); h++)
          {
            var lejer = new Lejer
            {
              LejemaalId = lejemaal.Id,
              FirstName = RimuTec.Faker.Name.FirstName(),
              MiddleName = RimuTec.Faker.Name.MiddleName(),
              LastName = RimuTec.Faker.Name.LastName(),
              Email = RimuTec.Faker.Internet.Email(),
              CellPhone = RimuTec.Faker.PhoneNumber.CellPhone(),
              LandLine = RimuTec.Faker.PhoneNumber.LandLine()
            };

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