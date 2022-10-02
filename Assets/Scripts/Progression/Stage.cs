using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
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
        unlockedItems.ForEach(x => Debug.Log(x.GetName() + " has been unlocked!"));
        return unlockedItems;
    }
}