using System.Collections.Generic;

public class Finder
{
    public static List<Recipe> FindRecipes(ItemType itemType)
    {
        return ScriptableObjectLoader.allRecipes.FindAll(x => x.Produces(itemType));
    }
}
