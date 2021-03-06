using UnityEngine;

public enum FadeDirection { None, FadeIn, FadeOut }

[RequireComponent(typeof(CanvasGroup))]
public class VisibilityHandler : MonoBehaviour
{
    private const float FadeSpeed = 10f;
    private bool isVisible;

    private FadeDirection fadeDirection;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
    }

    private void AnimateFadeIn()
    {
        if (canvasGroup.alpha >= 1) StopFading();
        else canvasGroup.alpha += Time.deltaTime * FadeSpeed;
    }

    private void AnimateFadeOut()
    {
        if (canvasGroup.alpha <= 0) StopFading();
        else canvasGroup.alpha -= Time.deltaTime * FadeSpeed;
    }

    private void StopFading()
    {
        fadeDirection = FadeDirection.None;
    }
}
