using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemLabel))]
public class TargetDisplay : MonoBehaviour
{
    private const string DisplayedText = "Craft this to unlock new technologies.";

    private ItemLabel itemLabel;
    private ItemType itemType;
    private bool isDisplaying = false;

    public ItemLabel ItemLabel => itemLabel = itemLabel != null ? itemLabel : GetComponent<ItemLabel>();

    public void Initialize(ItemType itemType)
    {
        this.itemType = itemType;
        UpdateDisplay();
        UnlockHandler.UnlockedStage += UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        if (!isDisplaying && UnlockHandler.instance.IsTargetItem(itemType))
            DisplayAsTarget();
        else if (isDisplaying && !UnlockHandler.instance.IsTargetItem(itemType))
            DisplayAsNotTarget();
    }

    private void OnDisable()
    {
        itemType = null;
        isDisplaying = false;
        UnlockHandler.UnlockedStage -= UpdateDisplay;
    }

    private void DisplayAsTarget()
    {
        ItemLabel.SetButtonColor(Palette.Yellow);
        ItemLabel.AddTooltipBuildingStep(DisplayTargetText);
        isDisplaying = true;
    }

    private void DisplayAsNotTarget()
    {
        ItemLabel.SetButtonColor(Palette.Button);
        ItemLabel.RemoveTooltipBuildingStep(DisplayTargetText);
        isDisplaying = false;
    }

    private void DisplayTargetText()
    {
        TooltipBuilder.instance.AddText(DisplayedText, Palette.Yellow);
    }
}
