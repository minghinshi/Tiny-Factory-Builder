using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemLabel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Counter counter;
    [SerializeField] private Button button;

    private bool isHoveredOver = false;
    private readonly List<Action> tooltipBuildingSteps = new();

    public Counter Counter
    {
        get
        {
            counter.gameObject.SetActive(true);
            return counter;
        }
    }

    private void OnDisable()
    {
        ResetImage();
        ResetCounter();
        ResetButton();
        ResetTooltips();
    }

    public void OnPointerEnter()
    {
        isHoveredOver = true;
        if (CanGenerateTooltips()) DisplayTooltip();
    }

    public void UpdateTooltip()
    {
        if (IsTooltipActive()) DisplayTooltip();
    }

    public void OnPointerExit()
    {
        isHoveredOver = false;
        if (CanGenerateTooltips()) Tooltip.instance.Hide();
    }

    public void AddImage(ItemType itemType)
    {
        image.gameObject.SetActive(true);
        image.sprite = itemType.GetSprite();
    }

    public void AddButton(params UnityAction[] onClick)
    {
        button.gameObject.SetActive(true);
        AddButtonAction(onClick);
    }

    public void AddButtonAction(params UnityAction[] onClick)
    {
        foreach (UnityAction action in onClick) button.onClick.AddListener(action);
    }

    public void DisplayCraftable(Process process)
    {
        PlayerInventory.instance.Changed += () => SetButtonInteractable(process.CanCraft());
    }

    public void SetButtonInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
    }

    public void AddTooltipBuildingSteps(params Action[] actions)
    {
        tooltipBuildingSteps.AddRange(actions);
    }

    private void DisplayTooltip()
    {
        Tooltip.instance.Show(tooltipBuildingSteps.ToArray());
    }

    private bool IsTooltipActive()
    {
        return isHoveredOver && CanGenerateTooltips();
    }

    private bool CanGenerateTooltips()
    {
        return tooltipBuildingSteps.Count > 0;
    }

    private void ResetImage()
    {
        image.sprite = null;
        image.gameObject.SetActive(false);
    }

    private void ResetCounter()
    {
        counter.gameObject.SetActive(false);
    }

    private void ResetButton()
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(false);
    }

    private void ResetTooltips()
    {
        if (IsTooltipActive()) OnPointerExit();
        tooltipBuildingSteps.Clear();
    }
}