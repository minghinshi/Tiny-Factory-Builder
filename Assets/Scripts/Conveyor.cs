using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Building
{
    private Cell thisCell;
    private Cell[] outputCells;

    public Conveyor(Cell primaryCell, Direction direction, ConveyorType conveyorType): base(primaryCell, direction, conveyorType) {
        Vector2Int gridPosition = primaryCell.GetGridPosition();
        GridSystem itemGrid = GridManager.itemGrid;
        thisCell = itemGrid.GetCellAt(gridPosition);

        Vector2Int[] outputPositions = conveyorType.GetOutputPositions();
        for (int i = 0; i < outputPositions.Length; i++)
        {
            outputCells[i] = itemGrid.GetCellAt(gridPosition + direction.RotateVector(outputPositions[i]));
        }

        TickHandler.instance.Tick += MoveItem;
    }

    private void MoveItem() {
        
    }
}
