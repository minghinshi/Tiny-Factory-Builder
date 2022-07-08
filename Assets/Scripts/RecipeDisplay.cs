using System;
using UnityEngine;

//TODO: reduce code duplication (though I have no idea how)
public class RecipeDisplay
{
    private readonly Transform transform;
    private readonly Recipe recipe;
    private Func<ItemStack, Transform> buildItemLabel;
    private Func<ItemType, Transform> buildMachineLabel;

    private RecipeDisplay(Transform transform, Recipe recipe, Func<ItemStack, Transform> buildItemLabel, Func<ItemType, Transform> buildMachineLabel)
    {
        this.transform = transform;
        this.recipe = recipe;
        this.buildItemLabel = buildItemLabel;
        this.buildMachineLabel = buildMachineLabel;
    }

    public static RecipeDisplay Create(Transform parent, Recipe recipe, Func<ItemStack, Transform> buildItemLabel, Func<ItemType, Transform> buildMachineLabel)
    {
        Transform transform = UnityEngine.Object.Instantiate(PrefabLoader.recipeDisplay, parent);
        RecipeDisplay recipeDisplay = new RecipeDisplay(transform, recipe, buildItemLabel, buildMachineLabel);
        recipeDisplay.ShowRecipe();
        return recipeDisplay;
    }

    private void ShowRecipe()
    {
        ShowInputs();
        ShowOutputs();
        ShowMachines();
    }

    private void ShowInputs()
    {
        ItemLabelGrid<ItemStack> inputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Inputs"));
        inputGrid.SetCreateLabelFunc(buildItemLabel);
        inputGrid.DisplayItems(recipe.GetInputs());
    }

    private void ShowOutputs()
    {
        ItemLabelGrid<ItemStack> outputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Outputs"));
        outputGrid.SetCreateLabelFunc(buildItemLabel);
        outputGrid.DisplayItems(recipe.GetOutputs());
    }

    private void ShowMachines()
    {
        ItemLabelGrid<ItemType> machineGrid = new ItemLabelGrid<ItemType>(transform.Find("Machines"));
        machineGrid.SetCreateLabelFunc(buildMachineLabel);
        machineGrid.DisplayItems(Finder.FindMachines(recipe).ConvertAll(x => (ItemType)x));
    }
}
