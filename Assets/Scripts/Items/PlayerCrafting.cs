using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    private ItemLabelGrid<Recipe> itemDisplay;
    [SerializeField] private List<Recipe> craftingRecipes;

    private void Start()
    {
        itemDisplay = new ItemLabelGrid<Recipe>(transform);
        itemDisplay.SetCreateLabelFunc(GenerateCraftingButton);
        itemDisplay.DisplayItems(craftingRecipes);
    }

    private Transform GenerateCraftingButton(Recipe recipe)
    {
        ItemLabelDirector.BuildItemButton(recipe.GetOutputs()[0].GetItemType(), () => Craft(recipe));
        return ItemLabelDirector.builder.GetResult();
    }

    private void Craft(Recipe recipe)
    {
        Inventory playerInventory = PlayerInventory.inventory;
        if (recipe.CanCraft(playerInventory)) recipe.CraftOnce(playerInventory, playerInventory);
    }
}