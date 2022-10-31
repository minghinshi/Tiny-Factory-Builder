using TMPro;
using UnityEngine;

public class GuideDisplay : MonoBehaviour
{
    public static GuideDisplay instance;

    [SerializeField] private TMP_Text mainText;
    [SerializeField] private TMP_Text sectionText;

    private VisibilityHandler visibilityHandler;
    [SerializeField] private VisibilityHandler previousPageButton;
    [SerializeField] private TMP_Text nextPageButtonText;

    private int currentPage;
    private Guide currentGuide;

    private void Awake()
    {
        instance = this;
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void DisplayGuide(Guide guide)
    {
        currentGuide = guide;
        SetPage(0);
        visibilityHandler.FadeIn();
    }

    public void OnPreviousButtonClicked()
    {
        SetPage(currentPage - 1);
    }

    public void OnNextButtonClicked()
    {
        if (IsOnLastPage()) CloseGuide();
        else SetPage(currentPage + 1);
    }

    private bool IsOnLastPage()
    {
        return currentPage + 1 == currentGuide.TotalPages;
    }

    private void CloseGuide()
    {
        visibilityHandler.FadeOut();
    }

    private void SetPage(int page)
    {
        currentPage = page;
        UpdateTexts();
        UpdatePreviousPageButton();
        UpdateNextPageButton();
    }

    private void UpdateTexts()
    {
        mainText.text = currentGuide.GetPage(currentPage);
        sectionText.text = string.Format("{0} - {1}/{2}", currentGuide.Name, currentPage + 1, currentGuide.TotalPages);
    }

    private void UpdatePreviousPageButton()
    {
        if (currentPage == 0) previousPageButton.SetInvisibleImmediately();
        else previousPageButton.SetVisibleImmediately();
    }

    private void UpdateNextPageButton()
    {
        if (IsOnLastPage()) nextPageButtonText.text = "??";
        else nextPageButtonText.text = ">";
    }
}
