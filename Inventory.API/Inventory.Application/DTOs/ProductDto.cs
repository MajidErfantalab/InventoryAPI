namespace Inventory.Application.DTOs;

public class ProductDto
{
    public long CompanyPrefix { get; set; }
    public string CompanyName { get; set; }
    public long ItemReference { get; set; }
    public string ProductName { get; set; }
}