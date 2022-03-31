using UnityEngine;

public class Conveyor : Building
{
    //TODO: Conveyor Optimization
    private Cell<Item> thisCell;
    private Cell<Item>[] outputCells;
    private int currentOutput = 0;

    public Conveyor(Vector2Int gridPosition, Direction direction, ConveyorType conveyorType) : base(gridPosition, direction, conveyorType)
    {
        GridSystem<Item> itemGrid = GridManager.itemGrid;
        thisCell = itemGrid.GetCellAt(gridPosition);
        thisCell.UnblockCell();

        Vector2Int[] outputPositions = conveyorType.GetOutputPositions();
        outputCells = new Cell<Item>[outputPositions.Length];
        for (int i = 0; i < outputPositions.Length; i++)
        {
            Vector2Int position = GetGridPositionFromOffset(outputPositions[i]);
            outputCells[i] = itemGrid.GetCellAt(position);
        }

        TickHandler.instance.TickConveyors += MoveItem;
    }

    private void MoveItem()
    {
        thisCell.MoveCellObjectTo(outputCells[currentOutput]);
        currentOutput = (currentOutput + 1) % outputCells.Length;
    }

    public override void Destroy()
    {
        thisCell.BlockCell();
        thisCell.DestroyCellObject();
        TickHandler.instance.TickConveyors -= MoveItem;

        base.Destroy();
    }
}
