using UnityEngine;
using UnityEngine.UI;

public enum FadeDirection { None, FadeIn, FadeOut }

[RequireComponent(typeof(CanvasGroup))]
public class VisibilityHandler : MonoBehaviour
{
    private const float FadeInTime = 0.3f;
    private const float FadeOutTime = 0.25f;
    private bool isVisible;

    private FadeDirection fadeDirection;
    private CanvasGroup canvasGroup;
    private LayoutElement layoutElement;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
    }

    private void Update()
    {
        switch (fadeDirection)
        {
            case FadeDirection.FadeIn:
                AnimateFadeIn();
                break;
            case FadeDirection.FadeOut:
                AnimateFadeOut();
                break;
        }
    }

    public void FadeIn()
    {
        fadeDirection = FadeDirection.FadeIn;
        SetVisible(true);
    }

    public void SetVisibleImmediately()
    {
        canvasGroup.alpha = 1;
        SetVisible(true);
    }

    public void FadeOut()
    {
        fadeDirection = FadeDirection.FadeOut;
        SetVisible(false);
    }

    public void SetInvisibleImmediately()
    {
        canvasGroup.alpha = 0;
        SetVisible(false);
    }

    public void Toggle()
    {
        if (isVisible) FadeOut();
        else FadeIn();
    }

    private void SetVisible(bool isVisible)
    {
        this.isVisible = isVisible;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
        if (layoutElement) layoutElement.ignoreLayout = !isVisible;
    }

    private void AnimateFadeIn()
    {
        if (canvasGroup.alpha >= 1) StopFading();
        else canvasGroup.alpha += Time.deltaTime / FadeInTime;
    }

    private void AnimateFadeOut()
    {
        if (canvasGroup.alpha <= 0) StopFading();
        else canvasGroup.alpha -= Time.deltaTime / FadeOutTime;
    }

    private void StopFading()
    {
        fadeDirection = FadeDirection.None;
    }
}
