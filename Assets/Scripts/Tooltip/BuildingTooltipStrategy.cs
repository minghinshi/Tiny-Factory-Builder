public class BuildingTooltipStrategy : TooltipStrategy
{
    private Building building;

    public BuildingTooltipStrategy(Building building) => this.building = building;

    public override bool UpdatedThisFrame()
    {
        return false;
    }

    protected override void BuildTooltip()
    {
        tooltipBuilder.AddText(building.GetBuildingType().GetName());
        if (building is Machine machine) BuildInventoryTooltip(machine.GetInputInventory(), "Inputs");
        if (building is Producer producer) BuildInventoryTooltip(producer.GetOutputInventory(), "Outputs");
    }

    private void BuildInventoryTooltip(Inventory inventory, string inventoryName)
    {
        tooltipBuilder.AddText(inventoryName + ":");
        tooltipBuilder.AddInventoryDisplay(inventory);
    }
}