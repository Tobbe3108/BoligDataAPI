namespace BoligDataAPI.Features.Lejer;

public record UpdateRequest(string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string CellPhone,
  string LandLine,
  DateTime MoveInDate,
  DateTime? MoveOutDate);