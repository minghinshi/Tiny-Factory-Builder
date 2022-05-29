using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    private Inventory targetInventory;
    [SerializeField] private Transform buttonPrefab;

    public delegate void ItemButtonPressedHandler(ItemType itemType);
    public event ItemButtonPressedHandler ButtonPressed;

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
        UpdateDisplay();
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory != null)
            targetInventory.Updated -= UpdateDisplay;
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.Updated += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        RemoveAllButtons();
        CreateButtons();
    }

    private void RemoveAllButtons()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void CreateButtons()
    {
        targetInventory.GetAllItemStacks().ForEach(x => ItemButtonMaker.instance.CreateItemButtonWithCounter(transform, x, () => ButtonPressed?.Invoke(x.GetItemType())));
    }
}
