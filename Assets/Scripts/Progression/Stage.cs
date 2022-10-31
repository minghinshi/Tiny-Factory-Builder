using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
[DataLoader("Data/Stages")]
public class Stage : ScriptableObject
{
    [SerializeField] private ItemType requiredItem;
    [SerializeField] private List<ItemType> unlockedItems;
    [SerializeField] private List<Guide> unlockedGuides;
    [SerializeField] private Guide displayedGuide;

    public Guide DisplayedGuide { get => displayedGuide; }

    public ItemType GetRequiredItem()
    {
        return requiredItem;
    }

    public List<ItemType> GetUnlockedItems()
    {
        return unlockedItems;
    }
}