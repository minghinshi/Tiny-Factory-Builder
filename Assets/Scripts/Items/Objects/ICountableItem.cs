using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICountableItem
{
    public string GetCountAsString();
    public ItemType GetItemType();
    public ItemStack GetItemStack();
    public ICountableItem MultiplyBy(int multiplier);
}
