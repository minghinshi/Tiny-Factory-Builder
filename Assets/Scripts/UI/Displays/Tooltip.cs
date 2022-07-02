using UnityEngine;

[RequireComponent(typeof(VisibilityHandler), typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;

    private Mouse mouse;
    private VisibilityHandler visibilityHandler;
    private TooltipDirector tooltipDirector;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mouse = Mouse.instance;
        visibilityHandler = GetComponent<VisibilityHandler>();
        tooltipDirector = new TooltipDirector(new TooltipBuilder(transform));

        mouse.TargetChanged += OnMouseTargetChanged;
    }

    public void ShowTooltip()
    {
        visibilityHandler.SetVisibleImmediately();
        tooltipDirector.BuildTooltip(mouse);
    }

    public void HideTooltip()
    {
        visibilityHandler.SetInvisibleImmediately();
    }

    private void OnMouseTargetChanged()
    {
        if (mouse.IsPointingAtSomething()) ShowTooltip();
        else HideTooltip();
    }
}