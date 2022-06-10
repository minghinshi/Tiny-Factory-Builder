using UnityEngine;

public enum FadeDirection { None, FadeIn, FadeOut }

[RequireComponent(typeof(CanvasGroup))]
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
        fadeDirection = FadeDirection.FadeIn;
        SetActive(true);
    }

    public void SetVisibleImmediately()
    {
        canvasGroup.alpha = 1;
        SetActive(true);
    }

    public void FadeOut()
    {
        fadeDirection = FadeDirection.FadeOut;
        SetActive(false);
    }

    public void SetInvisibleImmediately()
    {
        canvasGroup.alpha = 0;
        SetActive(false);
    }

    public void Toggle()
    {
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
