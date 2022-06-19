using System.Collections.Generic;

public class RecipeFinder
{
    public static List<Recipe> GetRecipes(ItemType itemType)
    {
        return DataLoader.GetAllRecipes().FindAll(x => x.Produces(itemType));
    }
}
