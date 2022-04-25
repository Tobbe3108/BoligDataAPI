namespace BoligDataAPI.Features.Database;

public record Lejemaal(Guid EjendomId,
  string StreetName,
  string BuildingNumber,
  string SecondaryAddress,
  string Postcode,
  string City,
  string State,
  string CountryCode,
  bool IsBookable) : ModelBase;