using System.Collections.Generic;

public class Finder
{
    public static List<Recipe> FindRecipes(ItemType itemType)
    {
        return DataLoader.allRecipes.FindAll(x => x.Produces(itemType));
    }

    public static List<MachineType> FindMachines(Recipe recipe)
    {
        return DataLoader.allMachines.FindAll(x => x.CanDo(recipe));
    }
}
