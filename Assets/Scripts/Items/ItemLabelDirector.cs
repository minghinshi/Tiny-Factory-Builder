using UnityEngine;
using UnityEngine.Events;

public class ItemLabelDirector
{
    public static readonly ItemLabelBuilder builder = new ItemLabelBuilder();
    public static readonly Color red = new Color(231f / 255, 76f / 255, 60f / 255);
    public static readonly Color yellow = new Color(241f / 255, 196f / 255, 15f / 255);

    public static void BuildItemButton(ItemType itemType, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        BuildItemLabel(itemType);
    }

    public static void BuildItemButton(ItemStack itemStack, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        BuildItemLabel(itemStack);
    }

    public static void BuildItemLabel(ItemType itemType)
    {
        builder.AddImage(itemType.GetSprite());
    }

    public static void BuildItemLabel(ItemStack itemStack)
    {
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
    }

    public static void BuildSingleCostLabel(ItemStack itemStack, Process process) {
        BuildItemLabel(itemStack);
        if (process.GetMissingItems().Contains(itemStack.GetItemType())) builder.SetTextColor(red);
    }

    public static void BuildBatchCostLabel(ItemStack itemStack, Process process)
    {
        BuildItemLabel(itemStack);
        if (process.GetLimitingItems().Contains(itemStack.GetItemType())) builder.SetTextColor(yellow);
    }

    public static void BuildCraftingButton(Process process, UnityAction craftingAction)
    {
        BuildItemButton(process.GetSingleOutput()[0].GetItemType(), craftingAction);
        builder.AddPointerEnterAction(() => Tooltip.instance.ShowTooltip(new CraftingTooltipStrategy(process)));
        builder.AddPointerExitAction(Tooltip.instance.HideTooltip);
    }
}
