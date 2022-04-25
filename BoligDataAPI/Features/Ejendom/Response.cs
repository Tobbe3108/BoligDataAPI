namespace BoligDataAPI.Features.Ejendom;

public record Response(Guid Id,
  string StreetName,
  string BuildingNumber,
  string Postcode,
  string City,
  string State,
  string CountryCode);
