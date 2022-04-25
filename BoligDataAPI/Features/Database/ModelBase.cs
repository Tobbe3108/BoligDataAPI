namespace BoligDataAPI.Features.Database;

public record ModelBase
{
  public Guid Id { get; init; } = Guid.NewGuid();
  public string ApiKey { get; set; } = null!;
}
