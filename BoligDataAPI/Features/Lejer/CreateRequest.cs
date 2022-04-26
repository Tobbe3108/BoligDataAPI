namespace BoligDataAPI.Features.Lejer;

public record CreateRequest(Guid LejemaalId,
  string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string CellPhone,
  string LandLine,
  DateTime MoveInDate,
  DateTime? MoveOutDate);