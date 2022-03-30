using UnityEngine;

public class Cell
{
    private bool blocked = false;
    private bool occupied = false;

    private Vector2Int position;

    private CellObject containedObject;
    private GridSystem gridSystem;

    public delegate void CellOccupiedHandler(Cell cell);
    public event CellOccupiedHandler CellOccupied;

    public Cell(int x, int y, GridSystem gridSystem, bool blocked)
    {
        position = new Vector2Int(x, y);
        this.gridSystem = gridSystem;
        this.blocked = blocked;
    }


    public Vector2Int GetGridPosition()
    {
        return position;
    }

    public Vector3 GetCornerWorldPosition()
    {
        return gridSystem.GetCornerWorldPosition(position);
    }

    public Vector3 GetCentreWorldPosition()
    {
        return gridSystem.GetCentreWorldPosition(position);
    }

    public CellObject GetContainedObject()
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

    public void OccupyCell(CellObject cellObject)
    {
        if (blocked) return;
        occupied = true;
        containedObject = cellObject;
        CellOccupied?.Invoke(this);
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

    public void MoveCellObjectTo(Cell destination)
    {
        if (!destination.CanInsert()) return;
        if (!occupied) return;
        if (containedObject.IsMovedThisTick()) return;

        containedObject.MoveTo(destination);
        destination.OccupyCell(containedObject);
        EmptyCell();
    }
}
