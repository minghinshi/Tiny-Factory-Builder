using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private Vector2Int position;
    private bool occupied = false;
    private CellObject containedObject;

    public Cell(int x, int y) {
        position = new Vector2Int(x, y);
    }

    public Vector2Int GetGridPosition() {
        return position;
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
}
