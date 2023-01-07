using System.Diagnostics;
using UnityEngine.Events;

public class ItemLabelBuilder
{
    public static ItemLabelBuilder instance = new();

    private ItemLabel label;

    private ItemLabel Label
    {
        get
        {
            if (label == null) label = ItemLabelPool.pool.Get();
            return label;
        }
    }

    private void RemoveLabel()
    {
        label = null;
    }

    public ItemLabel GetFinishedLabel()
    {
        ItemLabel output = Label;
        RemoveLabel();
        return output;
    }

    public void BuildLabelWithCounter(ICountableItem countableItem)
    {
        Label.AddImage(countableItem.GetItemType());
        Label.Counter.ShowCount(countableItem);
    }

    public void BuildCostLabel(ItemStack itemStack, Process process, bool doBatchCraft)
    {
        BuildLabelWithCounter(itemStack);
        Label.Counter.ShowAvailabilityOf(itemStack, process, doBatchCraft);
    }

    public void BuildGenericButton(ItemType itemType, params UnityAction[] onClick)
    {
        Label.AddButton(onClick);
        Label.AddImage(itemType);
        Label.AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddItemInfo(itemType));
    }

    public void BuildGenericButton(ICountableItem countableItem, params UnityAction[] onClick)
    {
        BuildGenericButton(countableItem.GetItemType(), onClick);
        Label.Counter.ShowCount(countableItem);
    }

    public void BuildCraftingButton(Process process, params UnityAction[] onClick)
    {
        BuildGenericButton(process.GetAverageSingleOutput()[0].GetItemType(), onClick);
        Label.AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddCraftingDisplay(process));
        Label.DisplayCraftable(process);
        Label.AddDisplayedAction(ActionsText.instance.craftItem);
        UpdateTooltipOnClick();
        UpdateTooltipOnShift();
    }

    public void BuildChangeDisplayLabel(InventoryChange change)
    {
        Label.AddImage(change.ItemType);
        Label.Counter.ShowChange(change);
    }

    private void UpdateTooltipOnClick()
    {
        Label.AddButtonAction(Label.UpdateTooltip);
    }

    private void UpdateTooltipOnShift()
    {
        KeyboardHandler.instance.ShiftPressed += Label.UpdateTooltip;
        KeyboardHandler.instance.ShiftReleased += Label.UpdateTooltip;
    }
}
