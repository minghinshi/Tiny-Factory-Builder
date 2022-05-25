using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private Inventory targetInventory;
    [SerializeField] private Transform buttonPrefab;

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        UpdateDisplay();
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory != null)
            targetInventory.InventoryUpdated -= UpdateDisplay;
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.InventoryUpdated += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        RemoveAllButtons();
        CreateButtons();
    }

    private void RemoveAllButtons()
    {
        foreach (Transform child in transform)
            Object.Destroy(child.gameObject);
    }

    private void CreateButtons()
    {
        foreach (ItemStack itemStack in targetInventory.GetAllItemStacks())
            CreateButton(itemStack);
    }

    private void CreateButton(ItemStack itemStack)
    {
        Transform buttonTransform = Object.Instantiate(buttonPrefab, transform);
        Image itemImage = buttonTransform.GetComponentInChildren<Image>();
        itemImage.sprite = itemStack.GetItemType().GetSprite();
        Text itemCounter = buttonTransform.GetComponentInChildren<Text>();
        itemCounter.text = itemStack.GetCount().ToString();
    }
}
