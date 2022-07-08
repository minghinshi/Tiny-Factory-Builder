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
        display.SetCreateLabelFunc(CreateLabel);
        display.SetTargetInventory(target);
    }

    public void AddRecipeDisplay(Recipe target)
    {
        RecipeDisplay.Create(transform, target, CreateLabel, CreateLabel);
    }

    private Transform CreateLabel(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemLabel(itemStack);
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform CreateLabel(ItemType itemType)
    {
        ItemLabelDirector.BuildItemLabel(itemType);
        return ItemLabelDirector.builder.GetResult();
    }
}
