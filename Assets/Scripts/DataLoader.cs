using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader
{
    public static readonly List<ItemType> allItemTypes = Resources.LoadAll<ItemType>("Data").ToList();
    public static readonly List<MachineType> allMachines = Resources.LoadAll<MachineType>("Data/Machines").ToList();
    public static readonly List<Recipe> allRecipes = Resources.LoadAll<Recipe>("Data/Recipes").ToList();
}
