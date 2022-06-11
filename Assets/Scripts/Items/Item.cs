using UnityEngine;

public class Item : CellObject
{
    private readonly ItemType itemType;

    public Item(Cell<Item> primaryCell, ItemType itemType)
    {
        gridPosition = primaryCell.GetGridPosition();
        this.itemType = itemType;
        transform = itemType.GetNewItemTransform(primaryCell.CentreWorldPosition);
        TickHandler.instance.TickItems += Tick;
        primaryCell.TryInsert(this);
    }

    public void Tick()
    {
        isMovedThisTick = false;
    }

    public override Vector2Int GetSize()
    {
        return new Vector2Int(1, 1);
    }

    public override void Destroy()
    {
        Object.Destroy(transform.gameObject);
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}
