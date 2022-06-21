using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayableAsItem { }

public abstract class ItemLabelGrid<T> : MonoBehaviour where T : IDisplayableAsItem
{
    private Func<T, Transform> CreateLabel;

    public void SetCreateLabelFunc(Func<T, Transform> func)
    {
        CreateLabel = func;
    }

    public void DisplayItems(List<T> items)
    {
        RemoveAllLabels();
        items.ForEach(DisplayItem);
    }

    private void RemoveAllLabels()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    private void DisplayItem(T item)
    {
        CreateLabel.Invoke(item).SetParent(transform);
    }
}

public class RecipeLabelGrid : ItemLabelGrid<Recipe> { }