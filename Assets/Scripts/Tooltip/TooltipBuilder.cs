using UnityEngine;
using UnityEngine.UI;

public class TooltipBuilder : MonoBehaviour
{
    public static TooltipBuilder instance;

    private void Awake()
    {
        instance = this;
    }

    public void ResetTooltip()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    public Text AddText(string str, Color color)
    {
        Text text = Instantiate(Prefabs.tooltipText, transform).GetComponent<Text>();
        text.text = str;
        text.color = color;
        return text;
    }

    public Text AddText(string str) => AddText(str, Palette.PrimaryText);

    public void AddItemInfo(ItemType itemType)
    {
        AddText(itemType.GetName());
        AddDescription(itemType);
    }

    public void AddInventoryDisplay(Inventory target)
    {
        InventoryDisplay display = Instantiate(Prefabs.inventoryDisplay, transform).GetComponent<InventoryDisplay>();
        display.SetBuildFunc(BuildItemLabel);
        display.SetTargetInventory(target);
    }

    public void BuildBuildingTooltip(Building building)
    {
        AddText(building.GetBuildingType().GetName());
        if (building is Machine machine) BuildInventoryTooltip(machine.GetInputInventory(), "Inputs");
        if (building is Producer producer) BuildInventoryTooltip(producer.GetOutputInventory(), "Outputs");
    }

    private void BuildInventoryTooltip(Inventory inventory, string inventoryName)
    {
        AddText(inventoryName + ":", Palette.SecondaryText);
        AddInventoryDisplay(inventory);
    }

    public void AddCraftingDisplay(Process process)
    {
        if (ShowBatchCraft(process)) AddBatchCraftDisplay(process);
        else AddSingleCraftDisplay(process);
    }

    private void AddDescription(ItemType itemType)
    {
        if (itemType.GetDescription() == "") return;
        AddText(itemType.GetDescription(), Palette.SecondaryText);
    }

    private bool ShowBatchCraft(Process process)
    {
        return Input.GetKey(KeyCode.LeftShift) && process.CanCraft();
    }

    private void AddSingleCraftDisplay(Process process)
    {
        RecipeDisplay display = RecipeDisplay.Create(transform);
        display.ShowInputs(x => BuildCostLabel(x, process, false), process.GetSingleInput());
        display.ShowOutputs(BuildItemLabel, process.GetAverageSingleOutput());
    }

    private void AddBatchCraftDisplay(Process process)
    {
        RecipeDisplay display = RecipeDisplay.Create(transform);
        display.ShowInputs(x => BuildCostLabel(x, process, true), process.GetBatchInput());
        display.ShowOutputs(BuildItemLabel, process.GetAverageBatchOutput());
    }

    private ItemLabel BuildItemLabel(ICountableItem countableItem)
    {
        return new ItemLabel.Builder().BuildLabelWithCounter(countableItem).Build();
    }

    private ItemLabel BuildCostLabel(ItemStack itemStack, Process process, bool showBatchCraft)
    {
        return new ItemLabel.Builder().BuildCostLabel(itemStack, process, showBatchCraft).Build();
    }
}
