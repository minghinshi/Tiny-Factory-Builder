using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemLabelDisplay : MonoBehaviour
{
    private Func<List<ItemLabel>> buildFunction;

    public void SetBuildFunc(Func<List<ItemLabel>> buildFunction)
    {
        this.buildFunction = buildFunction;
    }

    public void DisplayItemLabels()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
        buildFunction.Invoke().ForEach(x => x.transform.SetParent(transform));
    }
}