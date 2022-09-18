using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemLabel : MonoBehaviour
{
    private Image image;
    private Counter counter;
    private Button button;

    private bool generateTooltips = false;
    private bool isHoveredOver = false;
    private readonly List<Action> tooltipBuildingSteps = new();

    public static ItemLabel Create()
    {
        return Instantiate(PrefabLoader.emptyLabel).GetComponent<ItemLabel>();
    }

    public void OnPointerEnter()
    {
        isHoveredOver = true;
        if (generateTooltips) DisplayTooltip();
    }

    public void UpdateTooltip()
    {
        if (isHoveredOver && generateTooltips) DisplayTooltip();
    }

    public void OnPointerExit()
    {
        isHoveredOver = false;
        if (generateTooltips) Tooltip.instance.Hide();
    }

    public void AddImage(ItemType itemType)
    {
        image = Instantiate(PrefabLoader.image, transform).GetComponent<Image>();
        image.sprite = itemType.GetSprite();
    }

    public Counter GetCounter()
    {
        if (counter == null) counter = Instantiate(PrefabLoader.counter, transform).GetComponent<Counter>();
        return counter;
    }

    public void AddButton()
    {
        button = Instantiate(PrefabLoader.button, transform).GetComponent<Button>();
    }

    public void AddButton(params UnityAction[] onClick)
    {
        AddButton();
        AddButtonAction(onClick);
    }

    public void AddButtonAction(params UnityAction[] onClick)
    {
        foreach (UnityAction action in onClick) button.onClick.AddListener(action);
    }

    public void AddTooltipBuildingSteps(params Action[] actions)
    {
        generateTooltips = true;
        tooltipBuildingSteps.AddRange(actions);
    }

    private void DisplayTooltip()
    {
        Tooltip.instance.Show(tooltipBuildingSteps.ToArray());
    }
}
