namespace BoligDataAPI.Features.Lejer;

public record Response(Guid Id,
  Guid LejemaalId,
  string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string PhoneNumber,
  DateTime MoveInDate,
  DateTime? MoveOutDate);