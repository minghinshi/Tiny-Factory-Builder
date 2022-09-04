using UnityEngine;
using UnityEngine.UI;

public class TooltipBuilder : MonoBehaviour
{
    public void ResetTooltip()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    public void AddText(string str)
    {
        Instantiate(PrefabLoader.tooltipText, transform).GetComponent<Text>().text = str;
    }

    public void AddInventoryDisplay(Inventory target)
    {
        InventoryDisplay display = new(Instantiate(PrefabLoader.inventoryDisplay, transform));
        display.SetCreateLabelFunc(BuildItemLabel);
        display.SetTargetInventory(target);
    }

    public void AddSingleCraftDisplay(Process process)
    {
        RecipeDisplay display = RecipeDisplay.Create(transform);
        display.ShowInputs(x => BuildSingleCostLabel(x, process), process.GetSingleInput());
        display.ShowOutputs(BuildItemLabel, process.GetSingleOutput());
    }

    public void AddBatchCraftDisplay(Process process)
    {
        RecipeDisplay display = RecipeDisplay.Create(transform);
        display.ShowInputs(x => BuildBatchCostLabel(x, process), process.GetBatchInput());
        display.ShowOutputs(BuildItemLabel, process.GetBatchOutput());
    }

    private Transform BuildItemLabel(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemLabel(itemStack);
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform BuildSingleCostLabel(ItemStack itemStack, Process process)
    {
        ItemLabelDirector.BuildSingleCostLabel(itemStack, process);
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform BuildBatchCostLabel(ItemStack itemStack, Process process)
    {
        ItemLabelDirector.BuildBatchCostLabel(itemStack, process);
        return ItemLabelDirector.builder.GetResult();
    }
}
