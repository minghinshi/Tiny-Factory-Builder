public class TooltipDirector
{
    private readonly TooltipBuilder tooltipBuilder;

    public TooltipDirector(TooltipBuilder tooltipBuilder)
    {
        this.tooltipBuilder = tooltipBuilder;
    }

    public void BuildTooltip(Mouse mouse)
    {
        tooltipBuilder.ResetTooltip();
        if (mouse.IsPointingAtBuilding()) BuildBuildingTooltip(mouse.GetTargetBuilding());
        if (mouse.IsPointingAtItem()) BuildItemTooltip(mouse.GetTargetItem());
    }

    private void BuildBuildingTooltip(Building building)
    {
        tooltipBuilder.AddText("<b>" + building.GetBuildingType().GetName() + "</b>");
        if (building is Machine machine) BuildInventoryTooltip(machine.GetInputInventory(), "Inputs");
        if (building is Producer producer) BuildInventoryTooltip(producer.GetOutputInventory(), "Outputs");
    }

    private void BuildItemTooltip(Item item)
    {
        tooltipBuilder.AddText("Contains: " + item.GetItemType().GetName());
    }

    private void BuildInventoryTooltip(Inventory inventory, string inventoryName)
    {
        tooltipBuilder.AddText(inventoryName + ":");
        tooltipBuilder.AddInventoryDisplay(inventory);
    }
}
