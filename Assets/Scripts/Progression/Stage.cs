using System.Collections.Generic;
using UnityEngine;

public abstract class Stage : ScriptableObject
{
    private List<ItemType> unlockedItems;

    public List<ItemType> GetUnlockedItems()
    {
        return unlockedItems;
    }
}