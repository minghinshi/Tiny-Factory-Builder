using UnityEngine;

//A cell on a grid. Can contain objects.
public class Cell<TCellObject> where TCellObject : CellObject
{
    protected bool occupied = false;
    private bool blocked;

    protected Vector2Int gridPosition;
    protected TCellObject containedObject;

    public delegate void CellOccupiedHandler(Cell<TCellObject> thisCell);
    public event CellOccupiedHandler CellOccupied;

    public Cell(Vector2Int gridPosition, Vector3 worldPosition, bool startBlocked)
    {
        this.gridPosition = gridPosition;
        CentreWorldPosition = worldPosition;
        blocked = startBlocked;
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

    public void Insert(TCellObject cellObject)
    {
        occupied = true;
        containedObject = cellObject;
        CellOccupied?.Invoke(this);
    }

    public void TryInsert(TCellObject cellObject)
    {
        if (CanInsert()) Insert(cellObject);
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

    private bool CanMoveCellObjectTo(Cell<TCellObject> destination)
    {
        return destination.CanInsert() && occupied && !containedObject.IsMovedThisTick();
    }

    public void TryMoveCellObjectTo(Cell<TCellObject> destination)
    {
        if (CanMoveCellObjectTo(destination)) MoveCellObjectTo(destination);
    }

    private void MoveCellObjectTo(Cell<TCellObject> destination)
    {
        containedObject.MoveTo(destination.CentreWorldPosition);
        destination.Insert(containedObject);
        EmptyCell();
    }
}
