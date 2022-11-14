using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage")]
[DataLoader("Data/Stages")]
public class StageData : ScriptableObject
{
    public List<ItemStack> requiredItems;
    public List<ItemType> unlockedItems;
    [TextArea(3, 20)] public string guide;
}