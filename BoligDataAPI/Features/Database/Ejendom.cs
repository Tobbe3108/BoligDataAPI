namespace BoligDataAPI.Features.Database;

public record Ejendom(string StreetName,
  string BuildingNumber,
  string Postcode,
  string City,
  string State,
  string CountryCode) : ModelBase;