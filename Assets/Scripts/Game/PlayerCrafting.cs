using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    public static PlayerCrafting instance;

    private ItemLabelGrid<Process> itemDisplay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemDisplay = new ItemLabelGrid<Process>(transform);
        itemDisplay.SetCreateLabelFunc(GenerateCraftingButton);
    }

    public void UpdateDisplay()
    {
        itemDisplay.DisplayItems(GetProcesses());
    }

    private ItemLabel GenerateCraftingButton(Process process)
    {
        ItemLabelBuilder.instance.BuildCraftingButton(process, () => OnCraftingRequest(process));
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }

    private void OnCraftingRequest(Process process)
    {
        if (process.CanCraft()) Craft(process);
        else WarnCraftingFailed();
    }

    private void WarnCraftingFailed()
    {
        AudioHandler.instance.PlaySound(AudioHandler.instance.errorSound, false);
    }

    private void Craft(Process process)
    {
        if (Input.GetKey(KeyCode.LeftShift)) process.BatchCraft(); else process.CraftOnce();
        AudioHandler.instance.PlaySound(AudioHandler.instance.craftingSound, false);
    }

    private List<Process> GetProcesses()
    {
        List<Recipe> recipes = UnlockHandler.instance.GetUnlockedCraftingRecipes();
        return recipes.ConvertAll(x => new Process(x, Inventory.playerInventory, Inventory.playerInventory));
    }
}