using UnityEngine;

public static class Prefabs
{
    public static readonly Transform itemLabel = GetPrefab("ItemLabel");
    public static readonly Transform guideButton = GetPrefab("GuideButton");

    public static readonly Transform tooltipText = GetPrefab("Displays/Text");
    public static readonly Transform inventoryDisplay = GetPrefab("Displays/InventoryDisplay");
    public static readonly Transform recipeDisplay = GetPrefab("Displays/RecipeDisplay");
    public static readonly Transform changeDisplay = GetPrefab("Displays/ChangeDisplay");

    public static readonly Transform ghostVisuals = GetPrefab("Visuals/GhostVisuals");
    public static readonly Transform conveyorVisuals = GetPrefab("Visuals/ConveyorVisuals");
    public static readonly Transform producerVisuals = GetPrefab("Visuals/ProducerVisuals");

    private static Transform GetPrefab(string path)
    {
        return ((GameObject)Resources.Load("Prefabs/" + path)).transform;
    }
}