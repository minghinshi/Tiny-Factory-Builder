using UnityEngine;

public static class Prefabs
{
    public static readonly Transform button = GetPrefab("ItemLabel/Button");
    public static readonly Transform image = GetPrefab("ItemLabel/Image");
    public static readonly Transform counter = GetPrefab("ItemLabel/Counter");
    public static readonly Transform emptyLabel = GetPrefab("ItemLabel/ItemLabel");

    public static readonly Transform tooltipText = GetPrefab("Displays/Text");
    public static readonly Transform inventoryDisplay = GetPrefab("Displays/InventoryDisplay");
    public static readonly Transform recipeDisplay = GetPrefab("Displays/RecipeDisplay");

    public static readonly Transform ghostVisuals = GetPrefab("Visuals/GhostVisuals");
    public static readonly Transform conveyorVisuals = GetPrefab("Visuals/ConveyorVisuals");
    public static readonly Transform producerVisuals = GetPrefab("Visuals/ProducerVisuals");

    private static Transform GetPrefab(string path)
    {
        return ((GameObject)Resources.Load("Prefabs/" + path)).transform;
    }
}