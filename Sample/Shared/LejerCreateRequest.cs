namespace Sample.Shared;

public record LejerCreateRequest(Guid LejemaalId,
  string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string PhoneNumber,
  DateTime MoveInDate,
  DateTime? MoveOutDate);