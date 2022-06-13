using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static readonly Inventory inventory = new Inventory();

    [SerializeField] private ItemStack[] initialItems;

    private InventoryDisplay inventoryDisplay;
    private VisibilityHandler visibilityHandler;

    private void Start()
    {
        InitializeInventory();
        InitializeInventoryPanel();
        InitializeVisibilityHandler();
    }

    private void InitializeInventory()
    {
        foreach (ItemStack stack in initialItems) inventory.StoreCopyOf(stack);
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay = GetComponent<InventoryDisplay>();
        inventoryDisplay.SetCreateLabelFunc(CreateItemButton);
        inventoryDisplay.SetTargetInventory(inventory);
    }

    private void InitializeVisibilityHandler()
    {
        visibilityHandler = transform.parent.GetComponent<VisibilityHandler>();
    }

    private Transform CreateItemButton(ItemStack itemStack)
    {
        return ItemLabelDirector.CreateItemButton(itemStack, () => OnItemButtonPressed(itemStack.GetItemType()));
    }

    private void OnItemButtonPressed(ItemType itemType)
    {
        if (itemType is BuildingType buildingType && !OverrideControls()) StartPlacingBuilding(buildingType);
        else StartPlacingItem(itemType);
        visibilityHandler.SetInvisibleImmediately();
    }

    private void StartPlacingItem(ItemType itemType)
    {
        InputHandler.instance.SetPlacement(new ItemPlacement(itemType));
    }

    private void StartPlacingBuilding(BuildingType buildingType)
    {
        InputHandler.instance.SetPlacement(new BuildingPlacement(buildingType));
    }

    private bool OverrideControls()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}