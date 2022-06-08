using UnityEngine;

public enum FadeDirection { None, FadeIn, FadeOut }

public class VisibilityHandler : MonoBehaviour
{
    private const float FadeSpeed = 10f;
    private bool isActive;

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
        canvasGroup.alpha = 0;
        SetActive(true);
        fadeDirection = FadeDirection.FadeIn;
    }

    public void SetVisibleImmediately()
    {
        canvasGroup.alpha = 1;
        SetActive(true);
    }

    public void FadeOut()
    {
        canvasGroup.alpha = 1;
        SetActive(false);
        fadeDirection = FadeDirection.FadeOut;
    }

    public void SetInvisibleImmediately()
    {
        canvasGroup.alpha = 0;
        SetActive(false);
    }

    public void Toggle() {
        if (isActive) FadeOut();
        else FadeIn();
    }

    private void SetActive(bool isActive)
    {
        this.isActive = isActive;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    private void AnimateFadeIn()
    {
        canvasGroup.alpha += Time.deltaTime * FadeSpeed;
        if (canvasGroup.alpha >= 1) StopFading();
    }

    private void AnimateFadeOut()
    {
        canvasGroup.alpha -= Time.deltaTime * FadeSpeed;
        if (canvasGroup.alpha <= 0) StopFading();
    }

    private void StopFading()
    {
        fadeDirection = FadeDirection.None;
    }
}
