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
    }

    private void InitializeInventory()
    {
        foreach (ItemStack stack in initialItems)
            inventory.StoreCopyOf(stack);
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay = GetComponent<InventoryDisplay>();
        inventoryDisplay.SetTargetInventory(inventory);
        inventoryDisplay.ButtonPressed += OnItemButtonPressed;
        visibilityHandler = transform.parent.GetComponent<VisibilityHandler>();
    }

    private bool OverrideControls()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private void OnItemButtonPressed(ItemType itemType)
    {
        if (itemType is BuildingType buildingType && !OverrideControls()) StartPlacingBuilding(buildingType);
        else StartPlacingItem(itemType);
        visibilityHandler.SetInvisibleImmediately();
    }

    private void StartPlacingItem(ItemType itemType)
    {
        NewInputHandler.instance.SetPlacement(new ItemPlacement(itemType));
    }

    private void StartPlacingBuilding(BuildingType buildingType)
    {
        NewInputHandler.instance.SetPlacement(new BuildingPlacement(buildingType));
    }
}