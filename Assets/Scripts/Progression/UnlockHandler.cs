using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockHandler : MonoBehaviour
{
    public static UnlockHandler instance;

    private HashSet<Stage> lockedStages;
    private HashSet<ItemType> unlockedItems = new();
    private HashSet<Recipe> unlockedRecipes = new();

    public delegate void UnlockedStageHandler();
    public static event UnlockedStageHandler UnlockedStage;

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

    public List<ItemType> GetObtainableItems()
    {
        return unlockedItems.Where(CanProduce).ToList();
    }

    public List<Stage> GetUnlockedStages()
    {
        return GameData.allStages.Except(GetLockedStages()).ToList();
    }

    public void UnlockStage(Stage stage)
    {
        GetLockedStages().Remove(stage);
        UnlockItems(stage);
        UnlockRecipes();
        UnlockedStage?.Invoke();
    }

    private bool CanProduce(ItemType itemType)
    {
        return !unlockedRecipes.All(x => !x.Produces(itemType));
    }

    private void OnPlayerInventoryItemAdded(ItemType itemType)
    {
        GetLockedStages().ToList().FindAll(x => x.GetRequiredItem() == itemType).ForEach(UnlockStage);
    }

    private void UnlockItems(Stage stage)
    {
        unlockedItems.UnionWith(stage.GetUnlockedItems());
    }

    private void UnlockRecipes()
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
