namespace BoligDataAPI.Features.Lejer;

public record Response(Guid Id,
  Guid LejemaalId,
  string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string CellPhone,
  string LandLine,
  DateTime MoveInDate,
  DateTime? MoveOutDate);