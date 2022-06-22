using UnityEngine;
using UnityEngine.UI;

public class RecipeViewer : MonoBehaviour
{
    public static RecipeViewer instance;

    [SerializeField] private Transform itemPage, recipePage;
    [SerializeField] private PanelSwitcher panelSwitcher;
    [SerializeField] private Button returnButton;
    [SerializeField] private Text headerText;

    [SerializeField] private Transform recipeDisplayPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateButtons();
        returnButton.onClick.AddListener(ShowItemPage);
    }

    public void ViewRecipes(ItemType itemType)
    {
        ShowRecipePage(itemType);
        ClearRecipePage();
        Finder.FindRecipes(itemType).ForEach(CreateRecipeDisplay);
    }

    private void CreateRecipeDisplay(Recipe recipe)
    {
        new RecipeDisplay(Instantiate(recipeDisplayPrefab, recipePage), recipe).ShowRecipe();
    }

    private void CreateButtons()
    {
        DataLoader.allItemTypes.ForEach(CreateButton);
    }

    private void CreateButton(ItemType itemType)
    {
        ItemLabelDirector.BuildItemButton(itemType, () => ViewRecipes(itemType));
        ItemLabelDirector.builder.GetResult().SetParent(itemPage);
    }

    private void ShowItemPage()
    {
        panelSwitcher.SwitchPanel(itemPage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes";
    }

    private void ClearRecipePage()
    {
        foreach (Transform child in recipePage) Destroy(child.gameObject);
    }

    private void ShowRecipePage(ItemType itemType)
    {
        panelSwitcher.SwitchPanel(recipePage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes > " + itemType.GetName();
    }
}
