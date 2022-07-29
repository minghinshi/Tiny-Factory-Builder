using System;
using UnityEngine;

public class RecipeDisplay
{
    private readonly Transform transform;
    private readonly Recipe recipe;

    private RecipeDisplay(Transform transform, Recipe recipe)
    {
        this.transform = transform;
        this.recipe = recipe;
    }

    public static RecipeDisplay Create(Transform parent, Recipe recipe)
    {
        Transform transform = UnityEngine.Object.Instantiate(PrefabLoader.recipeDisplay, parent);
        RecipeDisplay recipeDisplay = new RecipeDisplay(transform, recipe);
        return recipeDisplay;
    }

    public void ShowInputs(Func<ItemStack, Transform> buildInputItemLabel)
    {
        ItemLabelGrid<ItemStack> inputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Inputs"));
        inputGrid.SetCreateLabelFunc(buildInputItemLabel);
        inputGrid.DisplayItems(recipe.GetInputs());
    }

    public void ShowOutputs(Func<ItemStack, Transform> buildOutputItemLabel)
    {
        ItemLabelGrid<ItemStack> outputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Outputs"));
        outputGrid.SetCreateLabelFunc(buildOutputItemLabel);
        outputGrid.DisplayItems(recipe.GetOutputs());
    }

    public void ShowMachines(Func<ItemType, Transform> buildMachineLabel)
    {
        ItemLabelGrid<ItemType> machineGrid = new ItemLabelGrid<ItemType>(transform.Find("Machines"));
        machineGrid.SetCreateLabelFunc(buildMachineLabel);
        machineGrid.DisplayItems(Finder.FindMachines(recipe).ConvertAll(x => (ItemType)x));
    }
}
