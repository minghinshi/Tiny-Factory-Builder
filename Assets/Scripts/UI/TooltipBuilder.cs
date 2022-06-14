using UnityEngine;
using UnityEngine.UI;

public class TooltipBuilder
{
    private readonly Transform textPrefab = ((GameObject)Resources.Load("Prefabs/Tooltip/Text")).transform;
    private readonly Transform inventoryDisplayPrefab = ((GameObject)Resources.Load("Prefabs/Tooltip/InventoryDisplay")).transform;

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
        Object.Instantiate(textPrefab, tooltipTransform).GetComponent<Text>().text = str;
    }

    public void AddInventoryDisplay(Inventory target)
    {
        InventoryDisplay display = Object.Instantiate(inventoryDisplayPrefab, tooltipTransform).GetComponent<InventoryDisplay>();
        display.SetCreateLabelFunc(CreateLabel);
        display.SetTargetInventory(target);
    }

    private Transform CreateLabel(ItemStack itemStack)
    {
        return ItemLabelDirector.BuildItemLabel(itemStack);
    }
}
