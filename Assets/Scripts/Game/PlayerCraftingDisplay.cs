using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemLabelDisplay))]
public class PlayerCraftingDisplay : MonoBehaviour
{
    public static PlayerCraftingDisplay instance;
    private ItemLabelDisplay itemDisplay;

    private void Awake()
    {
        instance = this;
        itemDisplay = GetComponent<ItemLabelDisplay>();
    }

    public void Initialize()
    {
        itemDisplay.SetBuildFunc(BuildCraftingButtons);
        UnlockHandler.UnlockedStage += itemDisplay.DisplayItemLabels;
    }

    private List<ItemLabel> BuildCraftingButtons()
    {
        return GetProcesses().ConvertAll(BuildCraftingButton);
    }

    private ItemLabel BuildCraftingButton(Process process)
    {
        return CraftingButton.Create(process).ItemLabel;
    }

    private List<Process> GetProcesses()
    {
        List<Recipe> recipes = UnlockHandler.instance.GetUnlockedCraftingRecipes();
        return recipes.ConvertAll(x => new Process(x, PlayerInventory.instance, PlayerInventory.instance));
    }
}