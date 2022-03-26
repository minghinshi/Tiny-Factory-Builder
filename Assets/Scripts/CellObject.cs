using UnityEngine;

public abstract class CellObject
{
    protected Cell primaryCell;
    protected Transform transform;

    public Vector2Int GetGridPosition()
    {
        return primaryCell.GetGridPosition();
    }

    public void MoveTo(Cell cell)
    {
        primaryCell = cell;
        transform.position = cell.GetCentreWorldPosition();
    }

    public abstract Vector2Int GetSize();
    public abstract void Destroy();
}
