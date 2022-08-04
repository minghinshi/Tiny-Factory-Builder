using UnityEngine;

public class CraftingTooltipStrategy : TooltipStrategy
{
    private Process process;

    public CraftingTooltipStrategy(Process process)
    {
        this.process = process;
    }

    public override bool UpdatedThisFrame()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift);
    }

    protected override void BuildTooltip()
    {
        if (ShowBatchCraft()) tooltipBuilder.AddBatchCraftDisplay(process);
        else tooltipBuilder.AddSingleCraftDisplay(process);
    }

    private bool ShowBatchCraft()
    {
        return Input.GetKey(KeyCode.LeftShift) && process.CanCraft();
    }
}