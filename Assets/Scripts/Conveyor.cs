using UnityEngine;

public class Conveyor : Building
{
    //TODO: Conveyor Optimization
    private Cell thisCell;
    private Cell[] outputCells;
    private int currentOutput = 0;

    public Conveyor(Cell primaryCell, Direction direction, ConveyorType conveyorType) : base(primaryCell, direction, conveyorType)
    {
        Vector2Int gridPosition = primaryCell.GetGridPosition();
        GridSystem itemGrid = GridManager.itemGrid;
        thisCell = itemGrid.GetCellAt(gridPosition);
        thisCell.UnblockCell();

        Vector2Int[] outputPositions = conveyorType.GetOutputPositions();
        outputCells = new Cell[outputPositions.Length];
        for (int i = 0; i < outputPositions.Length; i++)
        {
            Vector2Int outputGridPosition = GetGridPositionFromOffset(outputPositions[i]);
            outputCells[i] = itemGrid.GetCellAt(outputGridPosition);
        }

        TickHandler.instance.TickConveyors += MoveItem;
    }

    public void MoveItem()
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
