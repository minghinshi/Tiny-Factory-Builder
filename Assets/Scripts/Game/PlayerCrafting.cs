using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    private ItemLabelGrid<Recipe> itemDisplay;
    private Inventory playerInventory = PlayerInventory.inventory;
    [SerializeField] private List<Recipe> craftingRecipes;

    private void Start()
    {
        itemDisplay = new ItemLabelGrid<Recipe>(transform);
        itemDisplay.SetCreateLabelFunc(GenerateCraftingButton);
        itemDisplay.DisplayItems(craftingRecipes);
    }

    private Transform GenerateCraftingButton(Recipe recipe)
    {
        ItemLabelDirector.BuildCraftingButton(recipe, () => Craft(recipe));
        return ItemLabelDirector.builder.GetResult();
    }

    private void Craft(Recipe recipe)
    {
        if (!recipe.CanCraft(playerInventory)) return;
        if (Input.GetKey(KeyCode.LeftShift)) recipe.CraftAll(playerInventory, playerInventory);
        else recipe.CraftOnce(playerInventory, playerInventory);
    }
}