using UnityEngine;
using UnityEngine.Events;

public class ItemLabelDirector
{
    public static readonly ItemLabelBuilder builder = new ItemLabelBuilder();
    public static readonly Color red = new Color(231f / 255, 76f / 255, 60f / 255);
    public static readonly Color yellow = new Color(241f / 255, 196f / 255, 15f / 255);

    public static void BuildItemLabel(ItemType itemType)
    {
        builder.AddImage(itemType.GetSprite());
    }

    public static void BuildItemLabel(ItemStack itemStack)
    {
        BuildItemLabel(itemStack.GetItemType());
        builder.AddCounter(itemStack.GetCount().ToString("N0"));
    }

    public static void BuildItemLabel(ChanceOutput chanceOutput)
    {
        BuildItemLabel(chanceOutput.GetItemType());
        builder.AddCounter(chanceOutput.GetAverageItems().ToString("N2"));
        builder.SetCounterColor(yellow);
    }

    public static void BuildItemLabel(IRecipeOutput recipeOutput)
    {
        if (recipeOutput is ItemStack itemStack) BuildItemLabel(itemStack);
        else if (recipeOutput is ChanceOutput chanceOutput) BuildItemLabel(chanceOutput);
    }

    public static void BuildSingleCostLabel(ItemStack itemStack, Process process)
    {
        BuildItemLabel(itemStack);
        if (process.GetMissingItems().Contains(itemStack.GetItemType())) builder.SetCounterColor(red);
    }

    public static void BuildBatchCostLabel(ItemStack itemStack, Process process)
    {
        BuildItemLabel(itemStack);
        if (process.GetLimitingItems().Contains(itemStack.GetItemType())) builder.SetCounterColor(yellow);
    }

    public static void BuildCraftingButton(Process process, UnityAction craftingAction)
    {
        builder.AddButton(craftingAction);
        BuildItemLabel(process.GetSingleOutput()[0].GetItemType());
        builder.AddPointerEnterAction(() => Tooltip.instance.ShowTooltip(new CraftingTooltipStrategy(process)));
        builder.AddPointerExitAction(Tooltip.instance.HideTooltip);
    }
}
