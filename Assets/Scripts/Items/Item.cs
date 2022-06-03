using UnityEngine;

public class Item : CellObject
{
    private readonly ItemType itemType;

    public Item(Cell<Item> primaryCell, ItemType itemType)
    {
        gridPosition = primaryCell.GetGridPosition();
        primaryCell.TryInsert(this);
        this.itemType = itemType;
        transform = itemType.GetNewItemTransform(primaryCell.CentreWorldPosition);
        TickHandler.instance.TickItems += Tick;
    }

    public void Tick()
    {
        isMovedThisTick = false;
    }

    public override void Destroy()
    {
        Object.Destroy(transform.gameObject);
    }

    public override Vector2Int GetSize()
    {
        return new Vector2Int(1, 1);
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
