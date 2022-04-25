namespace BoligDataAPI.Features.Data;

public class DataFaker
{
  private readonly DataFakerConfiguration _dataFakerConfiguration;
  public List<Ejendom.Ejendom> Ejendomme { get; set; } = new();
  public List<Lejemaal.Lejemaal> Lejemaal { get; set; } = new();
  public List<Lejer.Lejer> Lejere { get; set; } = new();

  public DataFaker(DataFakerConfiguration dataFakerConfiguration)
  {
    _dataFakerConfiguration = dataFakerConfiguration;
    RimuTec.Faker.Config.Locale = "da-DK";
  }

  public void GenerateData()
  {
    for (var i = 0; i < _dataFakerConfiguration.NrOfEjendomme; i++)
    {
      var ejendom = new Ejendom.Ejendom(RimuTec.Faker.Address.StreetName(),
        RimuTec.Faker.Address.BuildingNumber(),
        RimuTec.Faker.Address.Postcode(),
        RimuTec.Faker.Address.City(),
        RimuTec.Faker.Address.State(),
        RimuTec.Faker.Address.CountryCode());

      for (var j = 0; j < GetRandomFromRange(_dataFakerConfiguration.RangeOfLejemaalPrEjendom); j++)
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

        for (var h = 0; h < GetRandomFromRange(_dataFakerConfiguration.RangeOfLejerePrLejemaal); h++)
        {
          var lejer = new Lejer.Lejer
          {
            LejemaalId = lejemaal.Id,
            FirstName = RimuTec.Faker.Name.FirstName(),
            MiddleName = RimuTec.Faker.Name.MiddleName(),
            LastName = RimuTec.Faker.Name.LastName(),
            Email = RimuTec.Faker.Internet.Email(),
            CellPhone = RimuTec.Faker.PhoneNumber.CellPhone(),
            LandLine = RimuTec.Faker.PhoneNumber.LandLine()
          };
          Lejere.Add(lejer);
        }

        Lejemaal.Add(lejemaal);
      }

      Ejendomme.Add(ejendom);
    }
  }

  private static int GetRandomFromRange(string rangeString)
  {
    var range = rangeString.Split(',');
    var value = Random.Shared.Next(int.Parse(range.First()), int.Parse(range.Last()) + 1);
    return value;
  }
}