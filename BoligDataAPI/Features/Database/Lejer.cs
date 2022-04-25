namespace BoligDataAPI.Features.Database;

public record Lejer : ModelBase
{
  //TODO Indflyt og freflyt dato
  public Guid LejemaalId { get; init; }
  public string FirstName { get; init; }
  public string MiddleName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string CellPhone { get; init; }
  public string LandLine { get; init; }
}