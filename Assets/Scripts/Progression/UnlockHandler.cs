using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockHandler : MonoBehaviour
{
    public static UnlockHandler instance;

    private HashSet<Stage> lockedStages;
    private HashSet<ItemType> unlockedItems = new();
    private HashSet<Recipe> unlockedRecipes = new();
    private HashSet<ItemType> targetItems;

    [SerializeField] private List<VisibilityHandler> hiddenElements;

    public delegate void UnlockedStageHandler();
    public static event UnlockedStageHandler UnlockedStage;

    private HashSet<Stage> LockedStages
    {
        get
        {
            lockedStages ??= new(GameData.allStages);
            return lockedStages;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerInventory.instance.ItemAdded += OnPlayerInventoryItemAdded;
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
        return GameData.allStages.Except(LockedStages).ToList();
    }

    public bool IsTargetItem(ItemType itemType)
    {
        return targetItems.Contains(itemType);
    }

    public void UnlockStage(Stage stage)
    {
        AddStage(stage);
        RewardItems(stage);
        UnlockedStage?.Invoke();
    }

    public void AddStage(Stage stage)
    {
        LockedStages.Remove(stage);
        UnlockItems(stage);
        UpdateTargetItems();
        UnlockRecipes();
        RevealElements(stage);
    }

    private bool CanProduce(ItemType itemType)
    {
        return !unlockedRecipes.All(x => !x.Produces(itemType));
    }

    private void OnPlayerInventoryItemAdded(ItemType itemType)
    {
        LockedStages.ToList().FindAll(x => x.requiredItem == itemType).ForEach(UnlockStage);
    }

    private void UnlockItems(Stage stage)
    {
        unlockedItems.UnionWith(stage.unlockedItems);
    }

    private void UpdateTargetItems()
    {
        targetItems = LockedStages.ToList().ConvertAll(x => x.requiredItem).FindAll(unlockedItems.Contains).ToHashSet();
    }

    private void UnlockRecipes()
    {
        unlockedRecipes.UnionWith(GameData.allRecipes.FindAll(IsRecipeUnlocked));
    }

    private bool IsRecipeUnlocked(Recipe recipe)
    {
        return recipe.GetRequiredItems().IsSubsetOf(unlockedItems);
    }

    private void RewardItems(Stage stage)
    {
        stage.rewards.ForEach(x => PlayerInventory.instance.StoreStack(x));
    }

    private void RevealElements(Stage stage)
    {
        stage.revealedElements.ForEach(x => hiddenElements[x].SetVisibleImmediately());
    }
}
