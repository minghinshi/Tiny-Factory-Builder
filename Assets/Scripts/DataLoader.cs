using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader
{
    public static List<ItemType> GetAllItemTypes()
    {
        return Resources.LoadAll<ItemType>("Data").ToList();
    }

    public static List<Recipe> GetAllRecipes()
    {
        return Resources.LoadAll<Recipe>("Data/Recipes").ToList();
    }
}
