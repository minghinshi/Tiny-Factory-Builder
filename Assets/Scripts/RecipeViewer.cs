using UnityEngine;
using UnityEngine.UI;

public class RecipeViewer : MonoBehaviour
{
    [SerializeField] private Transform itemPage, recipePage;
    [SerializeField] private PanelSwitcher panelSwitcher;
    [SerializeField] private Button returnButton;
    [SerializeField] private Text headerText;

    private void Start()
    {
        CreateButtons();
        returnButton.onClick.AddListener(ShowItemPage);
    }

    private void CreateButtons()
    {
        DataLoader.GetAllItemTypes().ForEach(CreateButton);
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

    private void ShowRecipePage(ItemType itemType)
    {
        panelSwitcher.SwitchPanel(recipePage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes > " + itemType.GetName();
    }

    private void ViewRecipes(ItemType itemType)
    {
        ShowRecipePage(itemType);
    }
}
