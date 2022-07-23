using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayableAsItem { }

public class ItemLabelGrid<T> where T : IDisplayableAsItem
{
    private Func<T, Transform> CreateLabel;
    private Transform transform;

    public ItemLabelGrid(Transform transform)
    {
        this.transform = transform;
    }

    public void SetCreateLabelFunc(Func<T, Transform> func)
    {
        CreateLabel = func;
    }

    public void DisplayItems(List<T> items)
    {
        if (transform)
        {
            ClearDisplay();
            items.ForEach(DisplayItem);
        }
        else Destroy();
    }

    protected virtual void Destroy() { }

    private void ClearDisplay()
    {
        foreach (Transform child in transform) UnityEngine.Object.Destroy(child.gameObject);
    }

    private void DisplayItem(T item)
    {
        CreateLabel.Invoke(item).SetParent(transform);
    }
}