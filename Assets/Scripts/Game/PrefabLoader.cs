using UnityEngine;

public static class PrefabLoader
{
    public static readonly Transform button = GetPrefab("ItemLabel/Button");
    public static readonly Transform image = GetPrefab("ItemLabel/Image");
    public static readonly Transform counter = GetPrefab("ItemLabel/Counter");
    public static readonly Transform emptyLabel = GetPrefab("ItemLabel/ItemLabel");

    public static readonly Transform tooltipText = GetPrefab("Displays/Text");
    public static readonly Transform inventoryDisplay = GetPrefab("Displays/InventoryDisplay");
    public static readonly Transform recipeDisplay = GetPrefab("Displays/RecipeDisplay");
    public static readonly Transform progressBar = GetPrefab("Displays/ProgressSlider");

    private static Transform GetPrefab(string path)
    {
        return ((GameObject)Resources.Load("Prefabs/" + path)).transform;
    }
}