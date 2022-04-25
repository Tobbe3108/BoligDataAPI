namespace BoligDataAPI.Features.Ejendom;

public record Ejendom(string StreetName,
  string BuildingNumber,
  string Postcode,
  string City,
  string State,
  string CountryCode)
{
  public Guid Id { get; init; } = Guid.NewGuid();
}