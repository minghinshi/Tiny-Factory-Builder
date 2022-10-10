using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockHandler : MonoBehaviour
{
    public static UnlockHandler instance;

    [SerializeField] private Stage defaultStage;

    private HashSet<Stage> lockedStages;
    private HashSet<ItemType> unlockedItems = new();
    private HashSet<Recipe> unlockedRecipes = new();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lockedStages = new(GameData.allStages);
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
        return GameData.allStages.Except(lockedStages).ToList();
    }

    public void UnlockStage(Stage stage)
    {
        lockedStages.Remove(stage);
        unlockedItems.UnionWith(stage.GetUnlockedItems());
        UpdateUnlockedRecipes();
        RecipeViewer.instance.ShowItems(unlockedItems.ToList());
        PlayerCrafting.instance.UpdateDisplay();
    }

    public void UnlockDefaultStage()
    {
        UnlockStage(defaultStage);
    }

    private void OnPlayerInventoryItemAdded(ItemType itemType)
    {
        lockedStages.ToList().FindAll(x => x.GetRequiredItem() == itemType).ForEach(UnlockStage);
    }

    private void UpdateUnlockedRecipes()
    {
        unlockedRecipes.UnionWith(GameData.allRecipes.FindAll(IsRecipeUnlocked));
    }

    private bool IsRecipeUnlocked(Recipe recipe)
    {
        return recipe.GetRequiredItems().IsSubsetOf(unlockedItems);
    }
}
