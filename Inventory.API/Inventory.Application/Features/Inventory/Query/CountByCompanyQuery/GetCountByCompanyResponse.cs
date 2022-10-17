namespace Inventory.Application.Features.Inventory.Query.CountByCompanyQuery;

public class GetCountByCompanyResponse
{
    public Int32 Count { get; set; }
    public string CompanyPrefix { get; set; }
}