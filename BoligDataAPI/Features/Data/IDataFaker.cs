namespace BoligDataAPI.Features.Data;

public interface IDataFaker
{
  void GenerateData(DataFakerConfiguration dataFakerConfiguration,
    IEnumerable<string> apiKeys);
}