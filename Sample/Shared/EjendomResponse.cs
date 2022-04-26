namespace Sample.Shared;

public record EjendomResponse(Guid Id,
  string StreetName,
  string BuildingNumber,
  string Postcode,
  string City,
  string State,
  string CountryCode);
