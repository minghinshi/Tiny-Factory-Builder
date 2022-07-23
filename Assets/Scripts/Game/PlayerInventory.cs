using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static readonly Inventory inventory = new Inventory();

    [SerializeField] private ItemStack[] initialItems;
    [SerializeField] private VisibilityHandler visibilityHandler;
    [SerializeField] private PanelSwitcher panelSwitcher;
    private InventoryDisplay inventoryDisplay;

    private void Start()
    {
        InitializeInventory();
        InitializeInventoryPanel();
    }

    private void InitializeInventory()
    {
        foreach (ItemStack stack in initialItems) inventory.StoreCopyOf(stack);
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay = new InventoryDisplay(transform);
        inventoryDisplay.SetCreateLabelFunc(CreateItemButton);
        inventoryDisplay.SetTargetInventory(inventory);
    }

    private Transform CreateItemButton(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemButton(itemStack, () => OnItemButtonPressed(itemStack.GetItemType()));
        return ItemLabelDirector.builder.GetResult();
    }

    private void OnItemButtonPressed(ItemType itemType)
    {
        if (itemType is BuildingType buildingType && !OverrideControls()) StartPlacingBuilding(buildingType);
        else StartPlacingItem(itemType);
        panelSwitcher.TogglePanel(visibilityHandler);
    }

    private void StartPlacingItem(ItemType itemType)
    {
        PlacementContext.instance.SetPlacement(new ItemPlacement(itemType));
    }

    private void StartPlacingBuilding(BuildingType buildingType)
    {
        PlacementContext.instance.SetPlacement(new BuildingPlacement(buildingType));
    }

    private bool OverrideControls()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}