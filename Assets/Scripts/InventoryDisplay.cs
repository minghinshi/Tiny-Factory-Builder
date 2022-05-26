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
            Destroy(child.gameObject);
    }

    private void CreateButtons()
    {
        foreach (ItemStack itemStack in targetInventory.GetAllItemStacks())
            CreateButton(itemStack);
    }

    private void CreateButton(ItemStack itemStack)
    {
        Transform buttonTransform = Instantiate(buttonPrefab, transform);
        SetButtonAppearance(itemStack, buttonTransform);
        ConnectButtonToEvent(itemStack, buttonTransform);
    }

    private void SetButtonAppearance(ItemStack itemStack, Transform buttonTransform)
    {
        Image itemImage = buttonTransform.GetChild(0).GetComponent<Image>();
        itemImage.sprite = itemStack.GetItemType().GetSprite();
        Text itemCounter = buttonTransform.GetChild(1).GetComponent<Text>();
        itemCounter.text = itemStack.GetCount().ToString();
    }

    private void ConnectButtonToEvent(ItemStack itemStack, Transform buttonTransform)
    {
        Button button = buttonTransform.GetComponent<Button>();
        button.onClick.AddListener(() => ButtonPressed?.Invoke(itemStack.GetItemType()));
    }
}
