using System.Collections.Generic;

public class PlayerInventory : Inventory
{
    public static PlayerInventory instance;

    public PlayerInventory(params ItemStack[] itemStacks)
    {
        this.itemStacks = new List<ItemStack>(itemStacks);
    }

    protected override void Store(ItemType itemType, int count)
    {
        base.Store(itemType, count);
        InventoryChangeDisplay.DisplayChange(new(itemType, count));
    }

    protected override void Remove(ItemType itemType, int count)
    {
        base.Remove(itemType, count);
        InventoryChangeDisplay.DisplayChange(new(itemType, -count));
    }
}
