namespace BoligDataAPI.Features.Lejer.Requests;

public record CreateRequest(Guid LejemaalId,
  string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string CellPhone,
  string LandLine);