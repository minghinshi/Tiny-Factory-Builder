using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
[DataLoader("Data/Stages")]
public class Stage : ScriptableObject
{
    [SerializeField] private ItemType requiredItem;
    [SerializeField] private List<ItemType> unlockedItems;

    public ItemType GetRequiredItem()
    {
        return requiredItem;
    }

    public List<ItemType> GetUnlockedItems()
    {
        return unlockedItems;
    }
}