using UnityEngine;

//TODO: reduce code duplication (though I have no idea how)
public class RecipeDisplay
{
    private readonly Transform transform;
    private readonly Recipe recipe;

    private RecipeDisplay(Transform transform, Recipe recipe)
    {
        this.transform = transform;
        this.recipe = recipe;
    }

    public static RecipeDisplay Create(Transform parent, Recipe recipe)
    {
        RecipeDisplay recipeDisplay = new RecipeDisplay(Object.Instantiate(PrefabLoader.recipeDisplay, parent), recipe);
        recipeDisplay.ShowRecipe();
        return recipeDisplay;
    }

    private void ShowRecipe()
    {
        ShowInputs();
        ShowOutputs();
        ShowMachines();
    }

    private void ShowInputs()
    {
        ItemLabelGrid<ItemStack> inputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Inputs"));
        inputGrid.SetCreateLabelFunc(CreateItemLabel);
        inputGrid.DisplayItems(recipe.GetInputs());
    }

    private void ShowOutputs()
    {
        ItemLabelGrid<ItemStack> outputGrid = new ItemLabelGrid<ItemStack>(transform.Find("Materials").Find("Outputs"));
        outputGrid.SetCreateLabelFunc(CreateItemLabel);
        outputGrid.DisplayItems(recipe.GetOutputs());
    }

    private void ShowMachines()
    {
        ItemLabelGrid<ItemType> machineGrid = new ItemLabelGrid<ItemType>(transform.Find("Machines"));
        machineGrid.SetCreateLabelFunc(CreateMachineLabel);
        machineGrid.DisplayItems(Finder.FindMachines(recipe).ConvertAll(x => (ItemType)x));
    }

    private Transform CreateItemLabel(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemButton(itemStack, () => RecipeViewer.instance.ViewRecipes(itemStack.GetItemType()));
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform CreateMachineLabel(ItemType itemType)
    {
        ItemLabelDirector.BuildItemButton(itemType, () => RecipeViewer.instance.ViewRecipes(itemType));
        return ItemLabelDirector.builder.GetResult();
    }
}
