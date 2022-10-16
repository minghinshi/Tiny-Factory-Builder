using UnityEngine;

[RequireComponent(typeof(InventoryDisplay))]
public class PlayerInventoryDisplay : MonoBehaviour
{
    public static PlayerInventoryDisplay instance;

    [SerializeField] private VisibilityHandler visibilityHandler;
    [SerializeField] private PanelSwitcher panelSwitcher;
    private InventoryDisplay inventoryDisplay;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        inventoryDisplay = GetComponent<InventoryDisplay>();
        InitializeInventoryPanel();
    }

    private void InitializeInventoryPanel()
    {
        inventoryDisplay.SetTargetInventory(PlayerInventory.instance);
        inventoryDisplay.SetBuildFunc(BuildItemButton);
    }

    private ItemLabel BuildItemButton(ItemStack itemStack)
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