using System;
using UnityEngine;

public class Gatherer : Producer
{
    private ItemType producedItem;

    public Gatherer(Vector2Int gridPosition, Direction direction, GathererType gathererType) : base(gridPosition, direction, gathererType)
    {
        producedItem = gathererType.producedItem;
    }

    public override bool CanInsert() => false;

    public override void Insert(ItemStack itemStack)
    {
        throw new InvalidOperationException();
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(150, true);
    }

    protected override void StoreOutputs()
    {
        outputInventory.Store(producedItem, 1);
    }
}
