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
        ItemLabelBuilder.instance.BuildCraftingButton(process, () => Craft(process));
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }

    private void Craft(Process process)
    {
        if (!process.CanCraft()) return;
        if (Input.GetKey(KeyCode.LeftShift)) process.BatchCraft();
        else process.CraftOnce();
    }

    private List<Process> GetProcesses()
    {
        List<Recipe> recipes = ScriptableObjectLoader.allCraftingRecipes;
        return recipes.ConvertAll(x => new Process(x, Inventory.playerInventory, Inventory.playerInventory));
    }
}