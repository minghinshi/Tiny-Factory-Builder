using System.Collections.Generic;

public class RecipeFinder
{
    public static List<Recipe> GetRecipes(ItemType itemType)
    {
        return DataLoader.allRecipes.FindAll(x => x.Produces(itemType));
    }
}
