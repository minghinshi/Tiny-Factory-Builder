using UnityEngine;

public class InventoryDisplay : ItemLabelGrid<ItemStack>
{
    private Inventory targetInventory;

    public InventoryDisplay(Transform transform) : base(transform) { }

    private void OnDestroy()
    {
        DisconnectFromInventory();
    }

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        DisplayInventory();
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
