using UnityEngine;

//A cell on a grid. Can be used to contain objects.
public class Cell
{
    protected bool occupied = false;
    protected Vector2Int position;
    protected CellObject containedObject;

    public Cell(Vector2Int position, GridSystem gridSystem)
    {
        this.position = position;
        CentreWorldPosition = gridSystem.GetCentreWorldPosition(position);
    }

    public Vector3 CentreWorldPosition { get; set; }

    //Returns the position of the cell.
    public Vector2Int GetGridPosition()
    {
        return position;
    }

    public CellObject GetContainedObject()
    {
        return containedObject;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public virtual bool CanInsert()
    {
        return !occupied;
    }

    public virtual void OccupyCell(CellObject cellObject)
    {
        occupied = true;
        containedObject = cellObject;
    }

    public void TryOccupyCell(CellObject cellObject)
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
}
