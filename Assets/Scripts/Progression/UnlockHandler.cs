using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockHandler : MonoBehaviour
{
    public static UnlockHandler instance;

    private HashSet<Stage> lockedStages;
    private HashSet<ItemType> unlockedItems = new();
    private HashSet<Recipe> unlockedRecipes = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Inventory.playerInventory.ItemAdded += OnPlayerInventoryItemAdded;
    }

    public List<Recipe> GetUnlockedRecipesFor(ItemType itemType)
    {
        return unlockedRecipes.ToList().FindAll(x => x.Produces(itemType));
    }

    public List<Recipe> GetUnlockedCraftingRecipes()
    {
        return GameData.allCraftingRecipes.FindAll(x => unlockedRecipes.Contains(x));
    }

    public bool CanProduce(ItemType itemType)
    {
        return !unlockedRecipes.All(x => !x.Produces(itemType));
    }

    public List<Stage> GetUnlockedStages()
    {
        return GameData.allStages.Except(GetLockedStages()).ToList();
    }

    public void UnlockStage(Stage stage)
    {
        GetLockedStages().Remove(stage);
        unlockedItems.UnionWith(stage.GetUnlockedItems());
        UpdateUnlockedRecipes();
        RecipeViewer.instance.ShowItems(unlockedItems.ToList());
        PlayerCrafting.instance.UpdateDisplay();
    }

    private void OnPlayerInventoryItemAdded(ItemType itemType)
    {
        GetLockedStages().ToList().FindAll(x => x.GetRequiredItem() == itemType).ForEach(UnlockStage);
    }

    private void UpdateUnlockedRecipes()
    {
        unlockedRecipes.UnionWith(GameData.allRecipes.FindAll(IsRecipeUnlocked));
    }

    private bool IsRecipeUnlocked(Recipe recipe)
    {
        return recipe.GetRequiredItems().IsSubsetOf(unlockedItems);
    }

    private HashSet<Stage> GetLockedStages()
    {
        lockedStages ??= new(GameData.allStages);
        return lockedStages;
    }
}
