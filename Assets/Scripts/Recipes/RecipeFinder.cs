using System.Collections.Generic;

public class Finder
{
    public static List<Recipe> FindRecipes(ItemType itemType)
    {
        return DataLoader.allRecipes.FindAll(x => x.Produces(itemType));
    }
}
