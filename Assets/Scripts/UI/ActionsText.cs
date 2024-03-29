using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionsText : MonoBehaviour
{
    public static ActionsText instance;

    public string[] basicActions;

    public string takeOutputs;
    public string takeAllItems;
    public string removeBuilding;

    public string[] placeBuilding;
    public string[] placeItem;
    public string[] craftItem;

    public string[] placingBuildingActions;
    public string[] placingItemActions;

    private TMP_Text text;
    private List<string> currentActions = new();

    private void Awake()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        UpdateActions();
        text.text = string.Join("\n", currentActions);
    }

    private void UpdateActions()
    {
        currentActions.Clear();
        currentActions.AddRange(basicActions);
        AddPlacementActions();
        AddButtonActions();
        if (Mouse.instance.GetTargetBuilding() != null)
            AddBuildingActions();
    }

    private void AddBuildingActions()
    {
        Building building = Mouse.instance.GetTargetBuilding();
        if (PlacementContext.instance.GetPlacement() is NullPlacement)
        {
            if (building is Producer) currentActions.Add(takeOutputs);
            if (building is Machine) currentActions.Add(takeAllItems);
        }
        currentActions.Add(removeBuilding);
    }

    private void AddPlacementActions() {
        if (PlacementContext.instance.GetPlacement() is BuildingPlacement)
            currentActions.AddRange(placingBuildingActions);
        else if (PlacementContext.instance.GetPlacement() is ItemPlacement)
            currentActions.AddRange(placingItemActions);
    }

    private void AddButtonActions()
    {
        ItemLabel label = Mouse.instance.TargetItemLabel;
        if (label != null)
        {
            if (label.TryGetComponent(out InventoryButton button))
                currentActions.AddRange(button.IsBuilding ? placeBuilding : placeItem);
            else if (label.GetComponent<CraftingButton>() != null)
                currentActions.AddRange(craftItem);
        }
    }
}
