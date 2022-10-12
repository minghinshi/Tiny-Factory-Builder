using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemLabelDisplay))]
public class PlayerCrafting : MonoBehaviour
{
    public static PlayerCrafting instance;
    private ItemLabelDisplay itemDisplay;

    private void Awake()
    {
        instance = this;
        itemDisplay = GetComponent<ItemLabelDisplay>();
    }

    private void Start()
    {
        itemDisplay.SetBuildFunc(BuildCraftingButtons);
    }

    public void UpdateDisplay()
    {
        itemDisplay.DisplayItemLabels();
    }

    private List<ItemLabel> BuildCraftingButtons() {
        return GetProcesses().ConvertAll(BuildCraftingButton);
    }

    private ItemLabel BuildCraftingButton(Process process)
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