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
        InventoryDisplay display = new InventoryDisplay(Instantiate(PrefabLoader.inventoryDisplay, transform));
        display.SetCreateLabelFunc(CreateItemLabel);
        display.SetTargetInventory(target);
    }

    public void AddRecipeDisplay(Recipe target)
    {
        RecipeDisplay.Create(transform, target, CreateStockIndicator, CreateItemLabel, CreateItemLabel);
    }

    private Transform CreateItemLabel(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemLabel(itemStack);
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform CreateStockIndicator(ItemStack itemStack)
    {
        ItemLabelDirector.BuildStockIndicator(itemStack);
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform CreateItemLabel(ItemType itemType)
    {
        ItemLabelDirector.BuildItemLabel(itemType);
        return ItemLabelDirector.builder.GetResult();
    }
}
