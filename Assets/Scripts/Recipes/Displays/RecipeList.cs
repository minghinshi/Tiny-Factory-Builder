using UnityEngine;

public class RecipeList : MonoBehaviour
{
    private RecipeBook recipeBook;

    private void Awake()
    {
        recipeBook = GetComponentInParent<RecipeBook>();
    }

    public void ViewRecipes(ItemType itemType)
    {
        ClearRecipePage();
        UnlockHandler.instance.GetUnlockedRecipesFor(itemType).ForEach(CreateRecipeDisplay);
    }

    private void ClearRecipePage()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    private void CreateRecipeDisplay(Recipe recipe)
    {
        RecipeDisplay display = RecipeDisplay.Create(transform);
        display.ShowInputs(recipeBook.BuildItemButton, recipe.GetInputs());
        display.ShowOutputs(recipeBook.BuildItemButton, recipe.GetAverageOutputs());
        display.ShowMachines(recipeBook.BuildItemButton, recipe.GetMachines());
    }
}
