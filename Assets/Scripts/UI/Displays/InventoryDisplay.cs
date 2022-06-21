public class InventoryDisplay : ItemLabelGrid<ItemStack>
{
    private Inventory targetInventory;

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

    private void DisplayInventory() {
        DisplayItems(targetInventory.GetAllItemStacks());
    }
}
