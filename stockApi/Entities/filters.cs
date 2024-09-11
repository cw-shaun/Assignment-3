using StockApi.Dto;

public class Filters
{
    public int? MinBudget { get; set; }
    public int? MaxBudget { get; set; }
    public List<int> FuelTypes { get; set; }
}
