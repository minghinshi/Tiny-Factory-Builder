using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private Vector2Int position;
    private bool occupied = false;
    private CellObject containedObject;
    private GridSystem gridSystem;

    public Cell(int x, int y, GridSystem gridSystem) {
        position = new Vector2Int(x, y);
        this.gridSystem = gridSystem;
    }

    public Vector2Int GetGridPosition() {
        return position;
    }

    public Vector3 GetCornerWorldPosition() {
        return gridSystem.GetCornerWorldPosition(position);
    }

    public Vector3 GetCentreWorldPosition() {
        return gridSystem.GetCentreWorldPosition(position);
    }

    public CellObject GetContainedObject() {
        return containedObject;
    }

    public bool isOccupied() {
        return occupied;    
    }

    public void OccupyCell(CellObject cellObject) {
        occupied = true;
        containedObject = cellObject;
    }

    public void EmptyCell() {
        occupied = false;
        containedObject = null;
    }

    public void MoveCellObjectTo(Cell destination) {
        if (destination.isOccupied()) return;

        containedObject.MoveTo(destination);
        destination.OccupyCell(containedObject);
        EmptyCell();
    }
}
