using System;
using System.Collections.Generic;

public class InventoryDisplay : ItemLabelDisplay
{
    private Inventory targetInventory;
    private Func<ItemStack, ItemLabel> buildFunc;

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        DisplayItemLabels();
    }

    public void SetBuildFunc(Func<ItemStack, ItemLabel> buildFunc)
    {
        this.buildFunc = buildFunc;
        SetBuildFunc(BuildItemLabels);
    }

    private void OnDestroy()
    {
        DisconnectFromInventory();
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.Updated += DisplayItemLabels;
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory != null) targetInventory.Updated -= DisplayItemLabels;
    }

    private List<ItemLabel> BuildItemLabels()
    {
        return targetInventory.GetAllItemStacks().ConvertAll(buildFunc.Invoke);
    }
}
