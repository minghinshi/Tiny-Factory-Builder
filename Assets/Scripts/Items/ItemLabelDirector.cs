using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemLabelDirector
{
    public static readonly ItemLabelBuilder builder = new ItemLabelBuilder();
    public static readonly Color red = new Color(231f / 255, 76f / 255, 60f / 255);

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

    public static void BuildStockIndicator(ItemStack itemStack)
    {
        BuildItemLabel(itemStack);
        if (!PlayerInventory.inventory.Contains(itemStack)) builder.SetTextColor(red);
    }

    public static void BuildCraftingButton(Recipe recipe, UnityAction craftingAction)
    {
        BuildItemButton(recipe.GetOutputs()[0].GetItemType(), craftingAction, () => Tooltip.instance.ShowTooltip(() => Tooltip.instance.BuildCraftingTooltip(recipe)));
        AddHoverTooltip(() => Tooltip.instance.BuildCraftingTooltip(recipe));
    }

    public static void AddHoverTooltip(Action buildTooltipAction)
    {
        builder.AddPointerEnterAction(() => Tooltip.instance.ShowTooltip(buildTooltipAction));
        builder.AddPointerExitAction(Tooltip.instance.HideTooltip);
    }
}
