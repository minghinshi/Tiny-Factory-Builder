using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    private ItemLabelGrid<Process> itemDisplay;

    private void Start()
    {
        itemDisplay = new ItemLabelGrid<Process>(transform);
        itemDisplay.SetCreateLabelFunc(GenerateCraftingButton);
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
        List<Recipe> recipes = ScriptableObjectLoader.allCraftingRecipes;
        return recipes.ConvertAll(x => new Process(x, Inventory.playerInventory, Inventory.playerInventory));
    }
}