using System;
using UnityEngine;

//TODO: reduce code duplication (though I have no idea how)
public class RecipeDisplay
{
    private readonly Transform transform;
    private readonly Recipe recipe;
    private Func<ItemStack, Transform> buildInputItemLabel;
    private Func<ItemStack, Transform> buildOutputItemLabel;
    private Func<ItemType, Transform> buildMachineLabel;

    private RecipeDisplay(Transform transform,
                          Recipe recipe,
                          Func<ItemStack, Transform> buildInputItemLabel,
                          Func<ItemStack, Transform> buildOutputItemLabel,
                          Func<ItemType, Transform> buildMachineLabel)
    {
        this.transform = transform;
        this.recipe = recipe;
        this.buildInputItemLabel = buildInputItemLabel;
        this.buildOutputItemLabel = buildOutputItemLabel;
        this.buildMachineLabel = buildMachineLabel;
    }

    public static RecipeDisplay Create(Transform parent,
                                       Recipe recipe,
                                       Func<ItemStack, Transform> buildInputItemLabel,
                                       Func<ItemStack, Transform> buildOutputItemLabel,
                                       Func<ItemType, Transform> buildMachineLabel)
    {
        Transform transform = UnityEngine.Object.Instantiate(PrefabLoader.recipeDisplay, parent);
        RecipeDisplay recipeDisplay = new RecipeDisplay(transform, recipe, buildInputItemLabel, buildOutputItemLabel, buildMachineLabel);
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
        inputGrid.SetCreateLabelFunc(buildInputItemLabel);
        inputGrid.DisplayItems(recipe.GetInputs());
    }

    private void ShowOutputs()
    {
        ItemLabelGrid<ItemStack> outputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Outputs"));
        outputGrid.SetCreateLabelFunc(buildOutputItemLabel);
        outputGrid.DisplayItems(recipe.GetOutputs());
    }

    private void ShowMachines()
    {
        ItemLabelGrid<ItemType> machineGrid = new ItemLabelGrid<ItemType>(transform.Find("Machines"));
        machineGrid.SetCreateLabelFunc(buildMachineLabel);
        machineGrid.DisplayItems(Finder.FindMachines(recipe).ConvertAll(x => (ItemType)x));
    }
}
