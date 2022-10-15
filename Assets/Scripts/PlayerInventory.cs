using System.Collections.Generic;

public class PlayerInventory : Inventory
{
    private List<InventoryChange> changesSinceLastFrame = new();

    protected override void Remove(ItemType itemType, int count)
    {
        base.Remove(itemType, count);
        changesSinceLastFrame.Add(new(itemType, -count));
    }

    protected override void Store(ItemType itemType, int count)
    {
        base.Store(itemType, count);
        changesSinceLastFrame.Add(new(itemType, count));
    }
}
