using UnityEngine;

public class Conveyor : Building
{
    //TODO: Conveyor Optimization
    private Cell thisCell;
    private Cell[] outputCells;
    private int currentOutput = 0;

    public Conveyor(Cell primaryCell, Direction direction, ConveyorType conveyorType) : base(primaryCell, direction, conveyorType)
    {
        Vector2Int primaryCellPosition = primaryCell.GetGridPosition();
        GridSystem itemGrid = GridManager.itemGrid;
        thisCell = itemGrid.GetCellAt(primaryCellPosition);
        thisCell.UnblockCell();

        Vector2Int[] outputPositions = conveyorType.GetOutputPositions();
        outputCells = new Cell[outputPositions.Length];
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
