using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ScriptableObjectLoader
{
    public static readonly ItemTypeLoader itemTypeLoader = ItemTypeLoader.Create();
    public static readonly List<MachineType> allMachines = GetObjects<MachineType>("Machines");
    public static readonly List<GathererType> starterMachines = GetObjects<GathererType>("Machines/Starters");
    public static readonly List<Recipe> allRecipes = GetObjects<Recipe>("Recipes");
    public static readonly List<Recipe> allCraftingRecipes = GetObjects<Recipe>("Recipes/Crafting");
    public static readonly List<DefaultStage> allDefaultStages = GetObjects<DefaultStage>("Stages");
    public static readonly List<LockedStage> allLockedStages = GetObjects<LockedStage>("Stages");

    private static List<T> GetObjects<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>("Data/" + path).ToList();
    }
}