using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeDisplay : ItemLabelDisplay
{
    private static InventoryChangeDisplay currentDisplay;
    private readonly List<InventoryChange> changes = new();

    public static void DisplayChange(InventoryChange inventoryChange)
    {
        GetCurrentDisplay().AddChange(inventoryChange);
    }

    private void Start()
    {
        SetBuildFunc(BuildLabels);
        currentDisplay = null;
        Destroy(gameObject, 0.5f);
    }

    private static InventoryChangeDisplay GetCurrentDisplay()
    {
        if (currentDisplay == null) Create();
        return currentDisplay;
    }

    private static void Create()
    {
        Transform displayTransform = Instantiate(Prefabs.changeDisplay, Transforms.UITransform);
        displayTransform.position = Input.mousePosition;
        currentDisplay = displayTransform.GetComponent<InventoryChangeDisplay>();
    }

    private void AddChange(InventoryChange inventoryChange)
    {
        changes.Add(inventoryChange);
    }

    private List<ItemLabel> BuildLabels()
    {
        return changes.ConvertAll(BuildLabel);
    }

    private ItemLabel BuildLabel(InventoryChange inventoryChange)
    {
        ItemLabelBuilder.instance.BuildChangeDisplayLabel(inventoryChange);
        return ItemLabelBuilder.instance.GetFinishedLabel();
    }
}
