using UnityEngine;

public static class PrefabLoader
{
    public static readonly Transform button = ((GameObject)Resources.Load("Prefabs/ItemLabel/Button")).transform;
    public static readonly Transform image = ((GameObject)Resources.Load("Prefabs/ItemLabel/Image")).transform;
    public static readonly Transform counter = ((GameObject)Resources.Load("Prefabs/ItemLabel/Counter")).transform;

    public static readonly Transform tooltipText = ((GameObject)Resources.Load("Prefabs/Displays/Text")).transform;
    public static readonly Transform inventoryDisplay = ((GameObject)Resources.Load("Prefabs/Displays/InventoryDisplay")).transform;
    public static readonly Transform recipeDisplay = ((GameObject)Resources.Load("Prefabs/Displays/RecipeDisplay")).transform;
    public static readonly Transform progressBar = ((GameObject)Resources.Load("Prefabs/Displays/ProgressSlider")).transform;
}