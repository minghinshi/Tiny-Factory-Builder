using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecipeOutput
{
    public ItemType GetItemType();
    public ItemStack GetItemStack();
    public IRecipeOutput MultiplyBy(int multiplier);
}
