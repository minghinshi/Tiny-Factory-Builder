using UnityEngine;

[RequireComponent(typeof(TooltipBuilder))]
public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    private TooltipStrategy tooltipStrategy;
    private VisibilityHandler visibilityHandler;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    private void Update()
    {
        if (tooltipStrategy != null && tooltipStrategy.UpdatedThisFrame()) tooltipStrategy.Execute();
    }

    public void SetStrategy(TooltipStrategy strategy)
    {
        tooltipStrategy = strategy;
    }

    public void ShowTooltip()
    {
        visibilityHandler.SetVisibleImmediately();
        tooltipStrategy.Execute();
    }

    public void ShowTooltip(TooltipStrategy strategy) {
        SetStrategy(strategy);
        ShowTooltip();
    }

    public void HideTooltip()
    {
        visibilityHandler.SetInvisibleImmediately();
    }
}
