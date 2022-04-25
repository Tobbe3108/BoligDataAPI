namespace BoligDataAPI.Features.Lejer.Requests;

public record UpdateRequest(string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string CellPhone,
  string LandLine);