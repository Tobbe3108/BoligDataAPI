namespace Sample.Shared;

public record LejerUpdateRequest(string FirstName,
  string MiddleName,
  string LastName,
  string Email,
  string PhoneNumber,
  DateTime MoveInDate,
  DateTime? MoveOutDate);