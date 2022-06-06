using System.Collections.Generic;
using UnityEngine;

public class PlayerCrafting : MonoBehaviour
{
    [SerializeField] private Transform buttonPrefab;
    [SerializeField] private List<Recipe> craftingRecipes;

    private void Start()
    {
        GenerateCraftingButtons();
    }

    private void GenerateCraftingButtons()
    {
        craftingRecipes.ForEach(x => ItemButtonMaker.instance.CreateItemButton(transform, x.GetOutputs()[0].GetItemType(), () => Craft(x)));
    }

    private void Craft(Recipe recipe)
    {
        Inventory playerInventory = PlayerInventory.inventory;
        if (recipe.CanCraft(playerInventory)) recipe.CraftOnce(playerInventory, playerInventory);
    }
}