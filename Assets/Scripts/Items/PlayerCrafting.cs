using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    [SerializeField] private List<Recipe> craftingRecipes;

    private void Start()
    {
        GenerateCraftingButtons();
    }

    private void GenerateCraftingButtons()
    {
        craftingRecipes.ForEach(x => GenerateCraftingButton(x));
    }

    private void GenerateCraftingButton(Recipe recipe)
    {
        Transform buttonTransform = ItemLabelDirector.BuildItemButton(recipe.GetOutputs()[0].GetItemType(), () => Craft(recipe));
        buttonTransform.SetParent(transform);
    }

    private void Craft(Recipe recipe)
    {
        Inventory playerInventory = PlayerInventory.inventory;
        if (recipe.CanCraft(playerInventory)) recipe.CraftOnce(playerInventory, playerInventory);
    }
}