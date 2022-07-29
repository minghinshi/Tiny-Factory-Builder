using UnityEngine;

public abstract class TooltipStrategy
{
    protected TooltipBuilder tooltipBuilder = Object.FindObjectOfType<TooltipBuilder>();

    public void Execute()
    {
        tooltipBuilder.ResetTooltip();
        BuildTooltip();
    }

    public abstract bool UpdatedThisFrame();
    protected abstract void BuildTooltip();
}