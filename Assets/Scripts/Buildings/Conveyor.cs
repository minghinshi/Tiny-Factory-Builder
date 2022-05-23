using UnityEngine;

public class Conveyor : Building
{
    //TODO: Conveyor Optimization
    private Cell<Item> itemCellAbove;
    private Cell<Item>[] outputCells;
    private int currentOutput = 0;

    public Conveyor(Vector2Int gridPosition, Direction direction, ConveyorType conveyorType) : base(gridPosition, direction, conveyorType)
    {
        SetItemCellAbove();
        SetOutputCells(conveyorType.GetOutputPositions());
        TickHandler.instance.TickConveyors += MoveItem;
    }

    private void SetOutputCells(Vector2Int[] outputPositions)
    {
        outputCells = new Cell<Item>[outputPositions.Length];
        for (int i = 0; i < outputPositions.Length; i++)
        {
            Vector2Int position = GetGridPositionFromOffset(outputPositions[i]);
            outputCells[i] = AllGrids.itemGrid.GetCellAt(position);
        }
    }

    private void SetItemCellAbove()
    {
        itemCellAbove = AllGrids.itemGrid.GetCellAt(gridPosition);
        itemCellAbove.UnblockCell();
    }

    private void MoveItem()
    {
        itemCellAbove.TryMoveCellObjectTo(outputCells[currentOutput]);
        currentOutput = (currentOutput + 1) % outputCells.Length;
    }

    public override void Destroy()
    {
        DestroyConveyor();
        base.Destroy();
    }

    private void DestroyConveyor()
    {
        itemCellAbove.BlockCell();
        itemCellAbove.DestroyCellObject();
        TickHandler.instance.TickConveyors -= MoveItem;
    }
}
