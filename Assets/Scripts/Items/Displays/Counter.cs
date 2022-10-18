using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Counter : MonoBehaviour
{
    private readonly Color red = new Color32(0xef, 0x9a, 0x9a, 0xff);
    private readonly Color yellow = new(241f / 255, 196f / 255, 15f / 255);
    private readonly Color green = new Color32(0xa5, 0xd6, 0xa7, 0xff);
    private Text text;

    public void ShowCount(ICountableItem countableItem)
    {
        GetText().text = countableItem.GetCountAsString();
        if (countableItem is ChanceOutput) SetColor(yellow);
    }

    public void ShowChange(InventoryChange inventoryChange)
    {
        GetText().text = inventoryChange.ToString();
        SetColor(inventoryChange.IsStoring() ? green : red);
    }

    public void ShowAvailabilityOf(ItemStack itemStack, Process process, bool doBatchCraft)
    {
        if (doBatchCraft) ColorLimitingItem(itemStack, process);
        else ColorMissingItem(itemStack, process);
    }

    private void SetColor(Color color)
    {
        text.color = color;
    }

    private Text GetText()
    {
        if (text == null) text = GetComponent<Text>();
        return text;
    }

    private void ColorLimitingItem(ItemStack itemStack, Process process)
    {
        if (process.IsLimiting(itemStack.GetItemType())) SetColor(yellow);
    }

    private void ColorMissingItem(ItemStack itemStack, Process process)
    {
        if (process.IsMissing(itemStack.GetItemType())) SetColor(red);
    }
}
