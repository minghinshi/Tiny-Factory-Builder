using UnityEngine;

//A cell on a grid. Can be used to contain objects.
public class Cell<TCellObject> where TCellObject : CellObject
{
    protected bool occupied = false;
    private bool blocked = true;

    protected Vector2Int gridPosition;
    protected TCellObject containedObject;

    public delegate void CellOccupiedHandler(Cell<TCellObject> thisCell);
    public event CellOccupiedHandler CellOccupied;

    public Cell(Vector2Int gridPosition, Vector3 worldPosition)
    {
        this.gridPosition = gridPosition;
        CentreWorldPosition = worldPosition;
    }

    public Vector3 CentreWorldPosition { get; set; }

    //Returns the position of the cell.
    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public TCellObject GetContainedObject()
    {
        return containedObject;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public bool IsBlocked()
    {
        return blocked;
    }

    public bool CanInsert()
    {
        return !(occupied || blocked);
    }

    public void OccupyCell(TCellObject cellObject)
    {
        occupied = true;
        containedObject = cellObject;
        CellOccupied?.Invoke(this);
    }

    public void TryOccupyCell(TCellObject cellObject)
    {
        if (CanInsert()) OccupyCell(cellObject);
    }

    public void EmptyCell()
    {
        occupied = false;
        containedObject = null;
    }

    public void DestroyCellObject()
    {
        if (occupied)
        {
            containedObject.Destroy();
            EmptyCell();
        }
    }

    public void BlockCell()
    {
        blocked = true;
    }

    public void UnblockCell()
    {
        blocked = false;
    }

    public void MoveCellObjectTo(Cell<TCellObject> destination)
    {
        if (!destination.CanInsert()) return;
        if (!occupied) return;
        if (containedObject.IsMovedThisTick()) return;

        containedObject.MoveTo(destination.CentreWorldPosition);
        destination.TryOccupyCell(containedObject);
        EmptyCell();
    }
}
