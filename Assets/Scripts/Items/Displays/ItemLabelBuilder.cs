using UnityEngine.Events;

public class ItemLabelBuilder
{
    private readonly ItemLabel label = ItemLabelPool.pool.Get();

    public ItemLabel Build()
    {
        return label;
    }

    public ItemLabelBuilder BuildLabelWithCounter(ICountableItem countableItem)
    {
        label.AddImage(countableItem.GetItemType());
        label.Counter.ShowCount(countableItem);
        return this;
    }

    public ItemLabelBuilder BuildCostLabel(ItemStack itemStack, Process process, bool doBatchCraft)
    {
        BuildLabelWithCounter(itemStack);
        label.Counter.ShowAvailabilityOf(itemStack, process, doBatchCraft);
        return this;
    }

    public ItemLabelBuilder BuildGenericButton(ItemType itemType, params UnityAction[] onClick)
    {
        label.AddButton(onClick);
        label.AddImage(itemType);
        label.AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddItemInfo(itemType));
        return this;
    }

    public ItemLabelBuilder BuildGenericButton(ICountableItem countableItem, params UnityAction[] onClick)
    {
        BuildGenericButton(countableItem.GetItemType(), onClick);
        label.Counter.ShowCount(countableItem);
        return this;
    }

    public ItemLabelBuilder BuildCraftingButton(Process process, params UnityAction[] onClick)
    {
        BuildGenericButton(process.GetAverageSingleOutput()[0].GetItemType(), onClick);
        label.AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddCraftingDisplay(process));
        label.DisplayCraftable(process);
        UpdateTooltipOnClick();
        UpdateTooltipOnShift();
        return this;
    }

    public ItemLabelBuilder BuildChangeDisplayLabel(InventoryChange change)
    {
        label.AddImage(change.ItemType);
        label.Counter.ShowChange(change);
        return this;
    }

    private void UpdateTooltipOnClick()
    {
        label.AddButtonAction(label.UpdateTooltip);
    }

    private void UpdateTooltipOnShift()
    {
        KeyboardHandler.instance.ShiftPressed += label.UpdateTooltip;
        KeyboardHandler.instance.ShiftReleased += label.UpdateTooltip;
    }
}
