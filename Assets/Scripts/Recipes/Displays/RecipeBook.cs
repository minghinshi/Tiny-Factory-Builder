using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    private RecipeSelection itemPage;
    private RecipeList recipePage;

    [SerializeField] private Button returnButton;
    [SerializeField] private Text headerText, returnText;
    [SerializeField] private PanelSwitcher tabSwitcher, panelSwitcher;

    private bool isShowingRecipe = false;

    private void Awake()
    {
        itemPage = GetComponentInChildren<RecipeSelection>();
        recipePage = GetComponentInChildren<RecipeList>();
    }

    public ItemLabel BuildItemButton(ItemType itemType)
    {
        ItemLabelBuilder.instance.BuildGenericButton(itemType, () => ViewRecipes(itemType));
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }

    public ItemLabel BuildItemButton(ICountableItem item)
    {
        ItemLabelBuilder.instance.BuildGenericButton(item, () => ViewRecipes(item.GetItemType()));
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }

    public void OnReturnButtonClicked()
    {
        if (isShowingRecipe) ShowItemPage();
        else ClosePanel();
    }

    private void ShowItemPage()
    {
        tabSwitcher.SwitchPanel(itemPage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes";
        returnText.text = "Close";
        isShowingRecipe = false;
    }

    private void ShowRecipePage(ItemType itemType)
    {
        tabSwitcher.SwitchPanel(recipePage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes > " + itemType.GetName();
        returnText.text = "Back";
        isShowingRecipe = true;
    }

    private void ViewRecipes(ItemType itemType)
    {
        ShowRecipePage(itemType);
        recipePage.ViewRecipes(itemType);
    }

    private void ClosePanel()
    {
        panelSwitcher.TogglePanel(GetComponent<VisibilityHandler>());
    }
}
