using System;
using UnityEngine.Events;

public class ItemLabelDirector
{
    public static readonly ItemLabelBuilder builder = new ItemLabelBuilder();

    public static void BuildItemButton(ItemType itemType, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemType.GetSprite());
    }

    public static void BuildItemButton(ItemStack itemStack, params UnityAction[] clickActions)
    {
        builder.AddButton(clickActions);
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
    }

    public static void BuildItemLabel(ItemStack itemStack)
    {
        builder.AddImage(itemStack.GetItemType().GetSprite());
        builder.AddCounter(itemStack.GetCount());
    }

    public static void AddHoverTooltip(Action buildTooltipAction)
    {
        builder.AddPointerEnterAction(() => Tooltip.instance.ShowTooltip(buildTooltipAction));
        builder.AddPointerExitAction(Tooltip.instance.HideTooltip);
    }
}
