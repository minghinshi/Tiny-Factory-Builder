using UnityEngine.Events;

public class ItemLabelBuilder
{
    public static ItemLabelBuilder instance = new();

    private ItemLabel label;

    private ItemLabel GetLabel()
    {
        if (label == null) label = ItemLabel.Create();
        return label;
    }

    private void RemoveLabel()
    {
        label = null;
    }

    public ItemLabel GetFinishedLabel()
    {
        ItemLabel output = GetLabel();
        RemoveLabel();
        return output;
    }

    public void BuildLabelWithCounter(ICountableItem countableItem)
    {
        GetLabel().AddImage(countableItem.GetItemType());
        GetLabel().GetCounter().ShowCountOf(countableItem);
    }

    public void BuildCostLabel(ItemStack itemStack, Process process, bool doBatchCraft)
    {
        BuildLabelWithCounter(itemStack);
        GetLabel().GetCounter().ShowAvailabilityOf(itemStack, process, doBatchCraft);
    }

    public void BuildGenericButton(ItemType itemType, params UnityAction[] onClick)
    {
        GetLabel().AddButton(onClick);
        GetLabel().AddImage(itemType);
        GetLabel().AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddItemInfo(itemType));
    }

    public void BuildGenericButton(ICountableItem countableItem, params UnityAction[] onClick)
    {
        BuildGenericButton(countableItem.GetItemType(), onClick);
        GetLabel().GetCounter().ShowCountOf(countableItem);
    }

    public void BuildCraftingButton(Process process, params UnityAction[] onClick)
    {
        BuildGenericButton(process.GetAverageSingleOutput()[0].GetItemType(), onClick);
        GetLabel().AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddCraftingDisplay(process));
        GetLabel().AddButtonAction(GetLabel().UpdateTooltip);
        KeyboardHandler.instance.ShiftPressed += GetLabel().UpdateTooltip;
        KeyboardHandler.instance.ShiftReleased += GetLabel().UpdateTooltip;
    }
}
