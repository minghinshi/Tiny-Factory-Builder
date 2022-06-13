using System;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private Func<ItemStack, Transform> createLabel;
    private Inventory targetInventory;

    public void SetCreateLabelFunc(Func<ItemStack, Transform> createLabel)
    {
        this.createLabel = createLabel;
    }

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        RebuildDisplay();
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory != null) targetInventory.Updated -= RebuildDisplay;
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.Updated += RebuildDisplay;
    }

    private void RebuildDisplay()
    {
        RemoveAllLabels();
        CreateLabels();
    }

    private void RemoveAllLabels()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    private void CreateLabels()
    {
        targetInventory.GetAllItemStacks().ForEach(x => CreateLabel(x));
    }

    private void CreateLabel(ItemStack itemStack)
    {
        Transform buttonTransform = createLabel.Invoke(itemStack);
        buttonTransform.SetParent(transform);
    }
}
