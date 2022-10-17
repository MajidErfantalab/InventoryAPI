namespace Inventory.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    //Company details could be a separate Entity for reusibility
    public string CompanyName { get; set; }

    public long CompanyPrefix { get; set; }

    public long ItemReference { get; set; }

    public string ProductName { get; set; }

    public DateTime Date { get; set; }

    public bool IsEnable { get; set; }

    public bool IsDeleted { get; set; }

    //public int? UserId { get; set; }
    
}