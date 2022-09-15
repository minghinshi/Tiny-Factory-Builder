using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private VisibilityHandler visibilityHandler;
    [SerializeField] private PanelSwitcher panelSwitcher;
    private InventoryDisplay inventoryDisplay;

    private void Start()
    {
        InitializeInventoryPanel();
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay = new InventoryDisplay(transform);
        inventoryDisplay.SetCreateLabelFunc(CreateItemButton);
        inventoryDisplay.SetTargetInventory(Inventory.playerInventory);
    }

    private ItemLabel CreateItemButton(ItemStack itemStack)
    {
        ItemLabelBuilder.instance.BuildGenericButton(itemStack, () => OnItemButtonPressed(itemStack.GetItemType()));
        return ItemLabelBuilder.instance.GetFinishedLabel();
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