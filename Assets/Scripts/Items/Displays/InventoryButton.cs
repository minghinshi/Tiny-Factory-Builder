using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private ItemLabel itemLabel;
    private ItemStack itemStack;
    private ItemType itemType;
    private bool isBuilding;

    public ItemLabel ItemLabel => itemLabel;
    public bool IsBuilding => isBuilding;

    public static InventoryButton Create(ItemStack itemStack)
    {
        ItemLabel itemLabel = ItemLabelPool.pool.Get();
        InventoryButton inventoryButton = itemLabel.CreateComponent<InventoryButton>();
        inventoryButton.itemLabel = itemLabel;
        inventoryButton.itemStack = itemStack;
        inventoryButton.Initialize();
        return inventoryButton;
    }

    private void Initialize()
    {
        itemLabel = new ItemLabel.Builder(itemLabel)
            .BuildGenericButton(itemStack, OnClick)
            .Build();
        itemType = itemStack.GetItemType();
        isBuilding = itemType is BuildingType;
    }

    private void OnClick()
    {
        if (PlaceAsBuilding())
            StartPlacingBuilding((BuildingType)itemType);
        else
            StartPlacingItem(itemType);
        PlayerInventoryDisplay.instance.ToggleDisplay();
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

    private bool PlaceAsBuilding()
    {
        return isBuilding && !OverrideControls();
    }
}
