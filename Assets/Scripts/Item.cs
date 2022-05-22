using UnityEngine;

public class Item : CellObject
{
    private ItemType itemType;

    /// <summary>
    /// Creates an item and places it at the location of the primaryCell.
    /// </summary>
    /// <param name="primaryCell">The cell that the item occupies.</param>
    /// <param name="itemType">The item type.</param>
    public Item(Cell<Item> primaryCell, ItemType itemType)
    {
        gridPosition = primaryCell.GetGridPosition();
        this.itemType = itemType;
        primaryCell.TryOccupyCell(this);
        transform = itemType.GetNewItemTransform(primaryCell.GetGridPosition());

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
