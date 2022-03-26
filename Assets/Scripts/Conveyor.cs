using UnityEngine;

public class Conveyor : Building
{
    private Cell thisCell;
    private Cell[] outputCells;

    public Conveyor(Cell primaryCell, Direction direction, ConveyorType conveyorType) : base(primaryCell, direction, conveyorType)
    {
        Vector2Int gridPosition = primaryCell.GetGridPosition();
        GridSystem itemGrid = GridManager.itemGrid;
        thisCell = itemGrid.GetCellAt(gridPosition);
        thisCell.UnblockCell();

        Vector2Int[] outputPositions = conveyorType.GetOutputPositions();
        for (int i = 0; i < outputPositions.Length; i++)
        {
            outputCells[i] = itemGrid.GetCellAt(gridPosition + direction.RotateVector(outputPositions[i]));
        }

        TickHandler.instance.Tick += MoveItem;
    }

    public void MoveItem()
    {
        //TEMPORARY
        Debug.Log("Moving item!");
    }

    public override void Destroy()
    {
        thisCell.BlockCell();
        thisCell.DestroyCellObject();
        base.Destroy();
    }
}
