namespace BoligDataAPI.Features.Data;

public record DataFakerConfiguration
{
  public int NrOfEjendomme { get; init; }
  public string RangeOfLejemaalPrEjendom { get; init; }
  public string RangeOfLejerePrLejemaal { get; init; }
}
