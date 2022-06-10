using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VisibilityHandler), typeof(RectTransform))]
public class Tooltip : MonoBehaviour
{
    private Mouse mouse;
    private VisibilityHandler visibilityHandler;

    //TODO: Make the tooltip include more than just text
    private Text text;

    private void Start()
    {
        mouse = Mouse.instance;
        visibilityHandler = GetComponent<VisibilityHandler>();
        text = transform.GetChild(0).GetComponent<Text>();
        mouse.TargetChanged += OnMouseTargetChanged;
    }

    private void OnMouseTargetChanged()
    {
        SetVisibility();
        SetText();
    }

    private void SetVisibility()
    {
        if (mouse.IsPointingAtBuilding() || mouse.IsPointingAtItem()) visibilityHandler.FadeIn();
        else visibilityHandler.FadeOut();
    }

    private void SetText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (mouse.IsPointingAtBuilding()) stringBuilder.Append(mouse.GetTargetBuilding().GetTooltipText());
        if (mouse.IsPointingAtItem()) stringBuilder.Append(mouse.GetTargetItem().GetTooltipText());
        text.text = stringBuilder.ToString();
    }
}