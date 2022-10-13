using System.Collections.Generic;

public class RecipeSelection : ItemLabelDisplay
{
    public static RecipeSelection instance;

    private RecipeBook recipeBook;

    private void Awake()
    {
        instance = this;
        recipeBook = GetComponentInParent<RecipeBook>();
    }

    public void Initialize()
    {
        SetBuildFunc(BuildButtons);
        UnlockHandler.UnlockedStage += DisplayItemLabels;
    }

    private List<ItemLabel> BuildButtons()
    {
        return UnlockHandler.instance.GetObtainableItems().ConvertAll(BuildButton);
    }

    private ItemLabel BuildButton(ItemType itemType)
    {
        return recipeBook.BuildItemButton(itemType);
    }
}