namespace BoligDataAPI.Features.Lejemaal;

public record Response(Guid Id,
  Guid EjendomId,
  string StreetName,
  string BuildingNumber,
  string SecondaryAddress,
  string Postcode,
  string City,
  string State,
  string CountryCode,
  bool IsBookable);
