using System.Collections;
using Inventory.Domain.Helpers;
using Inventory.Domain.Models;

namespace Inventory.Domain.Entities;

public class InventoryItem
{
    public int Id { get; set; }
    public int InventoryId { get; set; }

    public DateTime Date { get; set; }
    public string TagHex { get; set; }
    public string TagBinary { get; set; }

    public string TagCompanyPerfix { get; set; }
    public string TagItemReference { get; set; }
    public string TagSerialReference { get; set; }
    
    public virtual Inventory Inventory { get; set; }
    
    
    #region Helpers
    /// <summary>
    /// To be used for creation of new Inventory Item so that all tags be replaced
    /// </summary>
    public class InventoryItemBuilder : IBuilder<InventoryItem>
    {
        private readonly InventoryItem _inventoryItem;
        
        public InventoryItemBuilder SetTag(string TagHex)
        {
            _inventoryItem.TagHex = TagHex;
            
            //TO Do: Conversion Hexadecimal to Binary 
            _inventoryItem.TagBinary = ""; //Should be replaced
            
            // To Do: getting Company Prefix from Binary 
            _inventoryItem.TagCompanyPerfix = ""; //Should be replaced
            
            //To Do: Getting ItemReference from Binary 
            _inventoryItem.TagItemReference = ""; //Should be replaced
            
            //To Do: getting serial from Binary
            _inventoryItem.TagSerialReference = ""; //should be replaced

            return this;
        }

        
        public InventoryItemBuilder SetInventory(Inventory inventory)
        {
            _inventoryItem.InventoryId = inventory.Id;

            return this;
        }
        
        public InventoryItemBuilder SetDate(DateTime date)
        {
            _inventoryItem.Date = date;

            return this;
        }
        
        public InventoryItem Build()
        {
            return _inventoryItem;
        }
    }
    #endregion
}