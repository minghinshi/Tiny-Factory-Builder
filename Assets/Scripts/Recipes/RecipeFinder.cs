using System.Collections.Generic;

public class Finder
{
    public static List<Recipe> FindRecipes(ItemType itemType)
    {
        return GameDataHelper.allRecipes.FindAll(x => x.Produces(itemType));
    }
}
