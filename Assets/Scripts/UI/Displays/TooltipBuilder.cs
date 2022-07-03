using UnityEngine;
using UnityEngine.UI;

public class TooltipBuilder
{
    private readonly Transform tooltipTransform;

    public TooltipBuilder(Transform tooltipTransform)
    {
        this.tooltipTransform = tooltipTransform;
    }

    public void ResetTooltip()
    {
        foreach (Transform child in tooltipTransform) Object.Destroy(child.gameObject);
    }

    public void AddText(string str)
    {
        Object.Instantiate(PrefabLoader.tooltipText, tooltipTransform).GetComponent<Text>().text = str;
    }

    public void AddInventoryDisplay(Inventory target)
    {
        InventoryDisplay display = new InventoryDisplay(Object.Instantiate(PrefabLoader.inventoryDisplay, tooltipTransform));
        display.SetCreateLabelFunc(CreateLabel);
        display.SetTargetInventory(target); 
    }

    public void AddRecipeDisplay(Recipe target)
    {
        RecipeDisplay.Create(tooltipTransform, target);
    }

    private Transform CreateLabel(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemLabel(itemStack);
        return ItemLabelDirector.builder.GetResult();
    }
}
