using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeDisplay : MonoBehaviour
{
    [SerializeField] private ItemLabelDisplay inputGrid, outputGrid, machineGrid;
    [SerializeField] private GameObject textObject;

    public static RecipeDisplay Create(Transform parent)
    {
        return Instantiate(Prefabs.recipeDisplay, parent).GetComponent<RecipeDisplay>();
    }

    public void ShowInputs(Func<ItemStack, ItemLabel> buildInputLabel, List<ItemStack> inputs)
    {
        CreateItemLabelGrid(inputGrid, buildInputLabel, inputs);
    }

    public void ShowOutputs(Func<ICountableItem, ItemLabel> buildOutputLabel, List<ICountableItem> outputs)
    {
        CreateItemLabelGrid(outputGrid, buildOutputLabel, outputs);
    }

    public void ShowMachines(Func<MachineType, ItemLabel> buildMachineLabel, List<MachineType> machines)
    {
        if (machines.Count == 0) return;
        CreateItemLabelGrid(machineGrid, buildMachineLabel, machines);
        textObject.SetActive(true);
    }

    private void CreateItemLabelGrid<T>(ItemLabelDisplay grid, Func<T, ItemLabel> buildLabel, List<T> items)
    {
        grid.SetBuildFunc(() => items.ConvertAll(buildLabel.Invoke));
    }
}
