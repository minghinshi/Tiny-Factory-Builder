using System.Text;

public class TooltipBuilder
{
    private StringBuilder stringBuilder = new StringBuilder();

    public void AddBuildingInfo(Building building)
    {
        AddName("Building", building.GetBuildingType().GetName());
        if (building is Machine machine) AddInventoryDisplay(machine.GetInputInventory());
        if (building is Producer producer) AddInventoryDisplay(producer.GetOutputInventory());
    }

    public void AddItemInfo(Item item)
    {
        AddName("Item", item.GetItemType().GetName());
    }

    public string GetTooltip()
    {
        return stringBuilder.ToString();
    }

    private void AddName(string groupName, string objectName)
    {
        stringBuilder.Append("<b>").Append(groupName).Append(": ").Append(objectName).Append("</b>");
        stringBuilder.AppendLine();
    }

    private void AddInventoryDisplay(Inventory inventory)
    {
        //Placeholder method
    }
}
