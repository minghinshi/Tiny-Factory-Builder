using System;
using UnityEngine;
using System.Collections.Generic;

public class RecipeDisplay
{
    private readonly Transform inputTransform;
    private readonly Transform outputTransform;
    private readonly Transform machineTransform;

    private RecipeDisplay(Transform transform)
    {
        inputTransform = transform.Find("Materials").Find("Inputs");
        outputTransform = transform.Find("Materials").Find("Outputs");
        machineTransform = transform.Find("Machines");
    }

    public static RecipeDisplay Create(Transform parent)
    {
        Transform transform = UnityEngine.Object.Instantiate(PrefabLoader.recipeDisplay, parent);
        RecipeDisplay recipeDisplay = new RecipeDisplay(transform);
        return recipeDisplay;
    }

    public void ShowInputs(Func<ItemStack, Transform> buildInputLabel, List<ItemStack> inputs)
    {
        CreateItemLabelGrid(inputTransform, buildInputLabel, inputs);
    }

    public void ShowOutputs(Func<ItemStack, Transform> buildOutputLabel, List<ItemStack> outputs)
    {
        CreateItemLabelGrid(outputTransform, buildOutputLabel, outputs);
    }

    public void ShowMachines(Func<MachineType, Transform> buildMachineLabel, List<MachineType> machines)
    {
        CreateItemLabelGrid(machineTransform, buildMachineLabel, machines);
    }

    private void CreateItemLabelGrid<T>(Transform parent, Func<T, Transform> buildLabel, List<T> items) {
        ItemLabelGrid<T> grid = new ItemLabelGrid<T>(parent);
        grid.SetCreateLabelFunc(buildLabel);
        grid.DisplayItems(items);
     }
}