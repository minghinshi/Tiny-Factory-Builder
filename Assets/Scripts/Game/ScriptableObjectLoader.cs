using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ScriptableObjectLoader
{
    public static readonly ItemTypeLoader itemTypeLoader = ItemTypeLoader.Create();
    public static readonly List<MachineType> allMachines = Resources.LoadAll<MachineType>("Data/Machines").ToList();
    public static readonly List<GathererType> starterMachines = Resources.LoadAll<GathererType>("Data/Machines/Starters").ToList();
    public static readonly List<Recipe> allRecipes = Resources.LoadAll<Recipe>("Data/Recipes").ToList();
    public static readonly List<Recipe> allCraftingRecipes = Resources.LoadAll<Recipe>("Data/Recipes/Crafting").ToList();
}