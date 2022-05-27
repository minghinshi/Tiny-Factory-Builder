using UnityEngine;
using System.Collections.Generic;

public class Conveyor : Building
{
    //TODO: Conveyor Optimization
    private Cell<Item> itemCellAbove;
    private List<Cell<Item>> outputCells;
    private int currentOutput = 0;

    public Conveyor(Vector2Int gridPosition, Direction direction, ConveyorType conveyorType) : base(gridPosition, direction, conveyorType)
    {
        SetItemCellAbove();
        SetOutputCells(conveyorType.GetOutputPositions());
        TickHandler.instance.TickConveyors += MoveItem;
    }

    private void SetOutputCells(List<Vector2Int> relativePositions)
    {
        outputCells = RelativePositionsToCells(relativePositions);
    }

    private void SetItemCellAbove()
    {
        itemCellAbove = Grids.itemGrid.GetCellAt(gridPosition);
        itemCellAbove.UnblockCell();
    }

    private void MoveItem()
    {
        itemCellAbove.TryMoveCellObjectTo(outputCells[currentOutput]);
        currentOutput = (currentOutput + 1) % outputCells.Count;
    }

    public override void Destroy()
    {
        DestroyConveyor();
        base.Destroy();
    }

    private void DestroyConveyor()
    {
        itemCellAbove.BlockCell();
        StoreItemAbove();
        itemCellAbove.DestroyCellObject();
        TickHandler.instance.TickConveyors -= MoveItem;
    }

    private void StoreItemAbove() {
        if (itemCellAbove.GetContainedObject() != null)
            PlayerInventory.inventory.Store(itemCellAbove.GetContainedObject().GetItemType(), 1);
    }
}
