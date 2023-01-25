using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
[DataLoader("Data/Stages")]
public class Stage : ScriptableObject
{
    public ItemType requiredItem;

    public List<ItemType> unlockedItems;
    public List<ItemStack> rewards;
    public List<int> revealedElements;
}