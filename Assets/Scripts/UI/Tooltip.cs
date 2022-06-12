using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VisibilityHandler), typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    private Mouse mouse;
    private VisibilityHandler visibilityHandler;
    private RectTransform rectTransform;

    //TODO: Make the tooltip include more than just text
    private Text text;

    private void Start()
    {
        mouse = Mouse.instance;
        visibilityHandler = GetComponent<VisibilityHandler>();
        rectTransform = GetComponent<RectTransform>();
        text = transform.GetChild(0).GetComponent<Text>();

        mouse.TargetChanged += OnMouseTargetChanged;
    }

    private void OnMouseTargetChanged()
    {
        SetVisibility();
        SetText();
        SetFrameSize();
    }

    private void SetVisibility()
    {
        if (mouse.IsPointingAtBuilding() || mouse.IsPointingAtItem()) visibilityHandler.SetVisibleImmediately();
        else visibilityHandler.SetInvisibleImmediately();
    }

    private void SetText()
    {
        TooltipBuilder tooltipBuilder = new TooltipBuilder();
        if (mouse.IsPointingAtBuilding()) tooltipBuilder.AddBuildingInfo(mouse.GetTargetBuilding());
        if (mouse.IsPointingAtItem()) tooltipBuilder.AddItemInfo(mouse.GetTargetItem());
        text.text = tooltipBuilder.GetTooltip();
    }

    private void SetFrameSize()
    {
        const float Padding = 5f;
        rectTransform.sizeDelta = new Vector2(text.preferredWidth + Padding * 2, text.preferredHeight + Padding * 2);
    }
}