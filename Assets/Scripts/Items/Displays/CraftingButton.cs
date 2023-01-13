using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    private ItemLabel itemLabel;
    private Process process;

    public static CraftingButton Create(Process process)
    {
        ItemLabel itemLabel = ItemLabelPool.pool.Get();
        CraftingButton craftingButton = itemLabel.gameObject.AddComponent<CraftingButton>();
        craftingButton.itemLabel = itemLabel;
        craftingButton.process = process;
        craftingButton.Initialize();
        return craftingButton;
    }

    public ItemLabel GetItemLabel()
    {
        return itemLabel;
    }

    private void Initialize()
    {
        itemLabel = new ItemLabel.Builder(itemLabel)
            .BuildGenericButton(GetDisplayedItem(), OnCraftingRequest)
            .AddTooltipBuildingSteps(() => TooltipBuilder.instance.AddCraftingDisplay(process))
            .UpdateTooltipOnClick()
            .UpdateTooltipOnShift()
            .Build();
        DisplayCraftable();
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
