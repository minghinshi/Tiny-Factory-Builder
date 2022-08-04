using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> craftingRecipes;

    private ItemLabelGrid<Process> itemDisplay;
    private Inventory playerInventory = PlayerInventory.inventory;

    private void Start()
    {
        itemDisplay = new ItemLabelGrid<Process>(transform);
        itemDisplay.SetCreateLabelFunc(GenerateCraftingButton);
        itemDisplay.DisplayItems(GetProcesses());
    }

    private Transform GenerateCraftingButton(Process process)
    {
        ItemLabelDirector.BuildCraftingButton(process, () => Craft(process));
        return ItemLabelDirector.builder.GetResult();
    }

    private void Craft(Process process)
    {
        if (process.CanCraft())
        {
            if (Input.GetKey(KeyCode.LeftShift)) process.BatchCraft();
            else process.CraftOnce();
        }
    }

    private List<Process> GetProcesses() { 
        return craftingRecipes.ConvertAll(x => new Process(x, playerInventory, playerInventory));
    }
}