using UnityEngine;

public class CraftingTooltipStrategy : TooltipStrategy
{
    private Recipe recipe;

    public CraftingTooltipStrategy(Recipe recipe) => this.recipe = recipe;

    public override bool UpdatedThisFrame()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift);
    }

    protected override void BuildTooltip()
    {
        tooltipBuilder.AddSingleCraftDisplay(recipe);
    }
}