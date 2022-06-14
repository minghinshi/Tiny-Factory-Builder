using UnityEngine;

[RequireComponent(typeof(VisibilityHandler), typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    private Mouse mouse;
    private VisibilityHandler visibilityHandler;
    private TooltipDirector tooltipDirector;

    private void Start()
    {
        mouse = Mouse.instance;
        visibilityHandler = GetComponent<VisibilityHandler>();
        tooltipDirector = new TooltipDirector(new TooltipBuilder(transform));

        mouse.TargetChanged += OnMouseTargetChanged;
    }

    private void OnMouseTargetChanged()
    {
        if (mouse.IsPointingAtSomething()) ShowTooltip();
        else HideTooltip();
    }

    private void ShowTooltip()
    {
        visibilityHandler.SetVisibleImmediately();
        tooltipDirector.BuildTooltip(mouse);
    }

    private void HideTooltip()
    {
        visibilityHandler.SetInvisibleImmediately();
    }
}