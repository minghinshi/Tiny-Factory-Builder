using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private void ShowRecipePage(ItemType itemType)
    {
        panelSwitcher.SwitchPanel(recipePage.GetComponent<VisibilityHandler>());
        headerText.text = "Recipes > " + itemType.GetName();
    }

    private void ViewRecipes(ItemType itemType)
    {
        ShowRecipePage(itemType);
        List<Recipe> foundRecipes = Finder.FindRecipes(itemType);
        foundRecipes.ForEach(x => x.GetInputs().ForEach(y => print(y.GetCount().ToString() + " " + y.GetItemType().GetName())));
        foundRecipes.ForEach(x => Finder.FindMachines(x).ForEach(y => print(y.GetName())));
    }
}
