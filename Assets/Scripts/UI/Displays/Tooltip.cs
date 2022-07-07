using System;
using UnityEngine;

[RequireComponent(typeof(TooltipBuilder))]
public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    private TooltipBuilder tooltipBuilder;
    private VisibilityHandler visibilityHandler;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tooltipBuilder = GetComponent<TooltipBuilder>();
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void ShowTooltip(Action buildTooltipAction)
    {
        tooltipBuilder.ResetTooltip();
        visibilityHandler.SetVisibleImmediately();
        buildTooltipAction.Invoke();
    }

    public void HideTooltip()
    {
        visibilityHandler.SetInvisibleImmediately();
    }

    public void BuildCraftingTooltip(Recipe recipe)
    {
        tooltipBuilder.AddRecipeDisplay(recipe);
    }

    public void BuildBuildingTooltip(Building building)
    {
        tooltipBuilder.AddText("<b>" + building.GetBuildingType().GetName() + "</b>");
        if (building is Machine machine) BuildInventoryTooltip(machine.GetInputInventory(), "Inputs");
        if (building is Producer producer) BuildInventoryTooltip(producer.GetOutputInventory(), "Outputs");
    }

    public void BuildItemTooltip(Item item)
    {
        tooltipBuilder.AddText("Contains: " + item.GetItemType().GetName());
    }

    private void BuildInventoryTooltip(Inventory inventory, string inventoryName)
    {
        tooltipBuilder.AddText(inventoryName + ":");
        tooltipBuilder.AddInventoryDisplay(inventory);
    }
}
