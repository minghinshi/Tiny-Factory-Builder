using System;
using UnityEngine;

[RequireComponent(typeof(TooltipBuilder))]
public class Tooltip : MonoBehaviour
{
    public static Tooltip instance;
    private VisibilityHandler visibilityHandler;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void Show(params Action[] buildingSteps)
    {
        visibilityHandler.SetVisibleImmediately();
        TooltipBuilder.instance.ResetTooltip();
        foreach (Action buildingStep in buildingSteps) buildingStep.Invoke();
    }

    public void Hide()
    {
        visibilityHandler.SetInvisibleImmediately();
    }
}
