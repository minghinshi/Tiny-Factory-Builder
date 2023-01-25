using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemLabel : MonoBehaviour
{
    public class Builder
    {
        private readonly ItemLabel label;
        public Builder() => label = ItemLabelPool.pool.Get();
        public Builder(ItemLabel label) => this.label = label;
        public ItemLabel Build() => label;

        public Builder BuildLabelWithCounter(ICountableItem countableItem)
        {
            label.AddImage(countableItem.GetItemType());
            label.Counter.ShowCount(countableItem);
            return this;
        }

        public Builder BuildCostLabel(ItemStack itemStack, Process process, bool doBatchCraft)
        {
            BuildLabelWithCounter(itemStack);
            label.Counter.ShowAvailabilityOf(itemStack, process, doBatchCraft);
            return this;
        }

        public Builder BuildGenericButton(ItemType itemType, params UnityAction[] onClick)
        {
            label.AddButton(onClick);
            label.AddImage(itemType);
            AddTooltipBuildingStep(() => TooltipBuilder.instance.AddItemInfo(itemType));
            if (UnlockHandler.instance.IsTargetItem(itemType)) DisplayAsTargetItem();
            return this;
        }

        public Builder BuildGenericButton(ICountableItem countableItem, params UnityAction[] onClick)
        {
            BuildGenericButton(countableItem.GetItemType(), onClick);
            label.Counter.ShowCount(countableItem);
            return this;
        }

        public Builder BuildChangeDisplayLabel(InventoryChange change)
        {
            label.AddImage(change.ItemType);
            label.Counter.ShowChange(change);
            return this;
        }

        public Builder AddTooltipBuildingStep(Action action)
        {
            label.tooltipBuildingSteps.Add(action);
            return this;
        }

        public Builder UpdateTooltipOnClick()
        {
            label.AddButtonAction(label.UpdateTooltip);
            return this;
        }

        public Builder UpdateTooltipOnShift()
        {
            KeyboardHandler.instance.ShiftPressed += label.UpdateTooltip;
            KeyboardHandler.instance.ShiftReleased += label.UpdateTooltip;
            return this;
        }

        private void DisplayAsTargetItem()
        {
            string textToDisplay = "Craft this to unlock new technologies.";
            label.button.image.color = Palette.Yellow;
            AddTooltipBuildingStep(() => TooltipBuilder.instance.AddText(textToDisplay, Palette.Yellow));
        }
    }

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

    public void OnPointerEnter()
    {
        isHoveredOver = true;
        Mouse.instance.TargetItemLabel = this;
        if (CanGenerateTooltips()) DisplayTooltip();
    }

    public void UpdateTooltip()
    {
        if (IsTooltipActive()) DisplayTooltip();
    }

    public void OnPointerExit()
    {
        isHoveredOver = false;
        Mouse.instance.TargetItemLabel = null;
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

    public void SetButtonInteractable(bool isInteractable)
    {
        button.interactable = isInteractable;
    }

    public T CreateComponent<T>() where T : MonoBehaviour
    {
        if (TryGetComponent(out T component))
        {
            component.enabled = true;
            return component;
        }
        return gameObject.AddComponent<T>();
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

    private void OnDisable()
    {
        ResetImage();
        ResetCounter();
        ResetButton();
        ResetTooltips();
        ResetComponent<CraftingButton>();
        ResetComponent<InventoryButton>();
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
        button.image.color = Color.white;
        button.gameObject.SetActive(false);
    }

    private void ResetTooltips()
    {
        if (IsTooltipActive()) OnPointerExit();
        tooltipBuildingSteps.Clear();
    }

    private void ResetComponent<T>() where T : MonoBehaviour
    {
        if (TryGetComponent(out T component)) component.enabled = false;
    }
}