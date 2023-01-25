using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Counter : MonoBehaviour
{
    private ItemStack boundItemStack;

    private TMP_Text text;
    private TMP_Text Text
    {
        get
        {
            if (text == null) text = GetComponent<TMP_Text>();
            return text;
        }
    }

    private void OnDisable()
    {
        Text.text = "";
        SetColor(Palette.PrimaryText);
        if (boundItemStack != null) UnbindItemStack();
    }

    public void ShowCount(ICountableItem countableItem)
    {
        Text.text = countableItem.GetCountAsString();
        if (countableItem is ChanceOutput) SetColor(Palette.Yellow);
    }

    public void BindToItemStack(ItemStack itemStack)
    {
        boundItemStack = itemStack;
        itemStack.CountChanged += UpdateCount;
    }

    public void ShowChange(InventoryChange inventoryChange)
    {
        Text.text = inventoryChange.ToString();
        SetColor(inventoryChange.IsStoring() ? Palette.Green : Palette.Red);
    }

    public void ShowAvailabilityOf(ItemStack itemStack, Process process, bool doBatchCraft)
    {
        if (doBatchCraft) ColorLimitingItem(itemStack, process);
        else ColorMissingItem(itemStack, process);
    }

    private void UpdateCount()
    {
        ShowCount(boundItemStack);
    }

    private void SetColor(Color color)
    {
        text.color = color;
    }

    private void ColorLimitingItem(ItemStack itemStack, Process process)
    {
        if (process.IsLimiting(itemStack.GetItemType())) SetColor(Palette.Yellow);
    }

    private void ColorMissingItem(ItemStack itemStack, Process process)
    {
        if (process.IsMissing(itemStack.GetItemType())) SetColor(Palette.Red);
    }

    private void UnbindItemStack()
    {
        boundItemStack.CountChanged -= UpdateCount;
        boundItemStack = null;
    }
}
