using BoligDataAPI.Features.Database;

namespace BoligDataAPI.Features.Ejendom;

public record Ejendom(string StreetName,
  string BuildingNumber,
  string Postcode,
  string City,
  string State,
  string CountryCode) : ModelBase;