using UnityEngine;
using UnityEngine.UI;

public class RecipeViewer : MonoBehaviour
{
    public static RecipeViewer instance;

    [SerializeField] private Transform itemPage, recipePage;
    [SerializeField] private PanelSwitcher tabSwitcher, panelSwitcher;
    [SerializeField] private Button returnButton;
    [SerializeField] private Text headerText, returnText;

    private bool isShowingRecipe = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateButtons();
    }

    public void ViewRecipes(ItemType itemType)
    {
        ShowRecipePage(itemType);
        ClearRecipePage();
        Finder.FindRecipes(itemType).ForEach(CreateRecipeDisplay);
    }

    public void OnReturn()
    {
        if (isShowingRecipe) ShowItemPage();
        else ClosePanel();
    }

    private void CreateRecipeDisplay(Recipe recipe)
    {
        RecipeDisplay display = RecipeDisplay.Create(recipePage);
        display.ShowInputs(BuildItemButton, recipe.GetInputs());
        display.ShowOutputs(BuildItemButton, recipe.GetOutputs());
        display.ShowMachines(BuildMachineButton, recipe.GetMachines());
    }

    private Transform BuildItemButton(ItemStack itemStack)
    {
        ItemLabelDirector.BuildItemButton(itemStack, () => ViewRecipes(itemStack.GetItemType()));
        return ItemLabelDirector.builder.GetResult();
    }

    private Transform BuildMachineButton(ItemType itemType)
    {
        ItemLabelDirector.BuildItemButton(itemType, () => ViewRecipes(itemType));
        return ItemLabelDirector.builder.GetResult();
    }

    private void CreateButtons()
    {
        ScriptableObjectLoader.itemTypeLoader.GetAllItemTypes().ForEach(CreateButton);
    }

    private void CreateButton(ItemType itemType)
    {
        ItemLabelDirector.BuildItemButton(itemType, () => ViewRecipes(itemType));
        ItemLabelDirector.builder.GetResult().SetParent(itemPage);
    }

    private void ShowItemPage()
    {
        tabSwitcher.SwitchPanel(itemPage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes";
        returnText.text = "Close";
        isShowingRecipe = false;
    }

    private void ClearRecipePage()
    {
        foreach (Transform child in recipePage) Destroy(child.gameObject);
    }

    private void ShowRecipePage(ItemType itemType)
    {
        tabSwitcher.SwitchPanel(recipePage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes > " + itemType.GetName();
        returnText.text = "Back";
        isShowingRecipe = true;
    }

    private void ClosePanel()
    {
        panelSwitcher.TogglePanel(GetComponent<VisibilityHandler>());
    }
}
