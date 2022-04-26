namespace BoligDataAPI.Features.Lejer;

public record UpdateRequest(string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string PhoneNumber,
  DateTime MoveInDate,
  DateTime? MoveOutDate);