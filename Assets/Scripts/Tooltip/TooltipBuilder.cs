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

    public void AddText(string str)
    {
        Instantiate(PrefabLoader.tooltipText, transform).GetComponent<Text>().text = str;
    }

    public void AddItemInfo(ItemType itemType)
    {
        AddText(itemType.GetName());
        AddText(itemType.GetDescription());
    }

    public void AddInventoryDisplay(Inventory target)
    {
        InventoryDisplay display = new(Instantiate(PrefabLoader.inventoryDisplay, transform));
        display.SetCreateLabelFunc(BuildItemLabel);
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
        AddText(inventoryName + ":");
        AddInventoryDisplay(inventory);
    }

    public void AddCraftingDisplay(Process process)
    {
        if (ShowBatchCraft(process)) AddBatchCraftDisplay(process);
        else AddSingleCraftDisplay(process);
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

    private ItemLabel BuildItemLabel(ICountableItem recipeOutput)
    {
        ItemLabelBuilder.instance.BuildLabelWithCounter(recipeOutput);
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }

    private ItemLabel BuildCostLabel(ItemStack itemStack, Process process, bool showBatchCraft)
    {
        ItemLabelBuilder.instance.BuildCostLabel(itemStack, process, showBatchCraft);
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }
}
