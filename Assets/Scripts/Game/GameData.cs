using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameData
{
    public static readonly List<MachineType> allMachines = GetObjects<MachineType>("Machines");
    public static readonly List<Recipe> allRecipes = GetObjects<Recipe>("Recipes");
    public static readonly List<Recipe> allCraftingRecipes = GetObjects<Recipe>("Recipes/Crafting");
    public static readonly List<Stage> allStages = GetObjects<Stage>("Stages");

    public static readonly GathererType starterMachine = Resources.Load<GathererType>("Data/Machines/Starters/FireJar");
    public static readonly Stage rootStage = Resources.Load<Stage>("Data/Stages/Root");

    private static List<T> GetObjects<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>("Data/" + path).ToList();
    }
}