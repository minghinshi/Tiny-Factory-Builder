using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameData
{
    public static readonly List<MachineType> allMachines = GetObjects<MachineType>("Machines");
    public static readonly List<Recipe> allRecipes = GetObjects<Recipe>("Recipes");
    public static readonly List<Recipe> allCraftingRecipes = GetObjects<Recipe>("Recipes/Crafting");
    public static readonly List<Stage> allStages = GetObjects<Stage>("Stages");

    public static readonly List<GathererType> starterMachines = GetObjects<GathererType>("Machines/Starters");
    public static readonly Stage defaultStage = Resources.Load<Stage>("Data/Stages/Default");
    public static readonly Guide tutorial = Resources.Load<Guide>("Data/Guides/Tutorial");

    private static List<T> GetObjects<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>("Data/" + path).ToList();
    }
}