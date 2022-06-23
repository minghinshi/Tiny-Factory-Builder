using UnityEngine;

public class InventoryDisplay : ItemLabelGrid<ItemStack>
{
    private Inventory targetInventory;

    public InventoryDisplay(Transform transform) : base(transform) { }

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        DisplayInventory();
    }

    protected override void Destroy()
    {
        DisconnectFromInventory();
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.Updated += DisplayInventory;
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory != null) targetInventory.Updated -= DisplayInventory;
    }

    private void DisplayInventory()
    {
        DisplayItems(targetInventory.GetAllItemStacks());
    }
}
