using UnityEngine;

[RequireComponent(typeof(ItemLabel))]
public class CraftingButton : MonoBehaviour
{
    private ItemLabel itemLabel;
    private Process process;

    public ItemLabel ItemLabel => itemLabel;

    public static CraftingButton Create(Process process)
    {
        ItemLabel itemLabel = ItemLabelPool.pool.Get();
        CraftingButton craftingButton = itemLabel.CreateComponent<CraftingButton>();
        craftingButton.itemLabel = itemLabel;
        craftingButton.process = process;
        craftingButton.Initialize();
        return craftingButton;
    }

    private void Initialize()
    {
        itemLabel = new ItemLabel.Builder(itemLabel)
            .BuildGenericButton(GetDisplayedItem(), OnCraftingRequest)
            .UpdateTooltipOnClick()
            .UpdateTooltipOnShift()
            .Build();
        itemLabel.AddTooltipBuildingStep(() => TooltipBuilder.instance.AddCraftingDisplay(process));
        DisplayCraftable();
    }

    private void OnDisable()
    {
        PlayerInventory.instance.Changed -= UpdateCraftable;
    }

    private void DisplayCraftable()
    {
        UpdateCraftable();
        PlayerInventory.instance.Changed += UpdateCraftable;
    }

    private void UpdateCraftable()
    {
        itemLabel.SetButtonInteractable(process.CanCraft());
    }

    private ItemType GetDisplayedItem()
    {
        return process.GetAverageSingleOutput()[0].GetItemType();
    }

    private void OnCraftingRequest()
    {
        if (process.CanCraft()) Craft();
        else WarnCraftingFailed();
    }

    private void WarnCraftingFailed()
    {
        AudioHandler.instance.PlaySound(AudioHandler.instance.errorSound, false);
    }

    private void Craft()
    {
        if (Input.GetKey(KeyCode.LeftShift)) process.BatchCraft(); else process.CraftOnce();
        AudioHandler.instance.PlaySound(AudioHandler.instance.craftingSound, false);
    }
}
