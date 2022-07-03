using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader
{
    public static readonly List<ItemType> allItemTypes = Resources.LoadAll<ItemType>("Data").ToList();
    public static readonly List<MachineType> allMachines = Resources.LoadAll<MachineType>("Data/Machines").ToList();
    public static readonly List<Recipe> allRecipes = Resources.LoadAll<Recipe>("Data/Recipes").ToList();
}

public class PrefabLoader {
    public static readonly Transform button = ((GameObject)Resources.Load("Prefabs/ItemLabel/Button")).transform;
    public static readonly Transform image = ((GameObject)Resources.Load("Prefabs/ItemLabel/Image")).transform;
    public static readonly Transform counter = ((GameObject)Resources.Load("Prefabs/ItemLabel/Counter")).transform;

    public static readonly Transform tooltipText = ((GameObject)Resources.Load("Prefabs/Displays/Text")).transform;
    public static readonly Transform inventoryDisplay = ((GameObject)Resources.Load("Prefabs/Displays/InventoryDisplay")).transform;
    public static readonly Transform recipeDisplay = ((GameObject)Resources.Load("Prefabs/Displays/RecipeDisplay")).transform;
}
