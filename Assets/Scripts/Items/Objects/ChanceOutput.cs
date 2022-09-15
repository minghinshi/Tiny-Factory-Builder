using System;
using UnityEngine;

[Serializable]
public class ChanceOutput : ICountableItem
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private float averageItems;

    public ChanceOutput(ItemType itemType, float averageItems)
    {
        this.itemType = itemType;
        this.averageItems = averageItems;
    }

    public ChanceOutput() : this(null, 0f) { }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public float GetAverageItems()
    {
        return averageItems;
    }

    public string GetCountAsString()
    {
        return averageItems.ToString("N2");
    }

    public ICountableItem MultiplyBy(int multiplier)
    {
        return new ChanceOutput(itemType, averageItems * multiplier);
    }

    public ItemStack GetItemStack()
    {
        return new ItemStack(itemType, GetBaseItems() + GetBonusOutput());
    }

    private int GetBaseItems()
    {
        return (int)averageItems;
    }

    private int GetBonusOutput()
    {
        if (UnityEngine.Random.value < (averageItems % 1)) return 1;
        return 0;
    }
}
