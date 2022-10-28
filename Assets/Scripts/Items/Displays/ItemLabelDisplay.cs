using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemLabelDisplay : MonoBehaviour
{
    private Func<List<ItemLabel>> buildFunction;

    public void SetBuildFunc(Func<List<ItemLabel>> buildFunction)
    {
        this.buildFunction = buildFunction;
        DisplayItemLabels();
    }

    public void DisplayItemLabels()
    {
        if (buildFunction == null) Debug.LogError("Build function has not been set!");
        DisableLabels();
        BuildLabels();
    }

    private void DisableLabels()
    {
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            ItemLabelPool.pool.Release(child.GetComponent<ItemLabel>());
        }
    }

    private void BuildLabels()
    {
        buildFunction.Invoke().ForEach(x => x.transform.SetParent(transform));
    }
}