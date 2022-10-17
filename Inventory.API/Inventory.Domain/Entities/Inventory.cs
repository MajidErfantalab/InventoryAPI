using System.Collections.Generic;

namespace Inventory.Domain.Entities;

public class Inventory
{
    public Inventory()
    {
        InventoryItems = new HashSet<InventoryItem>();
    }
    public int Id { get; set; }
    
    public string InventoryLocation { get; set; }

    public string InventoryReferenceId { get; set; }

    public DateTime InventoryDate { get; set; }
    public ICollection<InventoryItem> InventoryItems { get; set; }

}