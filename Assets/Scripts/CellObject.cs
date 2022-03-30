using UnityEngine;

public abstract class CellObject
{
    protected Cell primaryCell;
    protected Transform transform;
    protected bool isMovedThisTick;

    public Vector2Int GetGridPosition()
    {
        return primaryCell.GetGridPosition();
    }

    public void MoveTo(Cell cell)
    {
        primaryCell = cell;
        transform.position = cell.GetCentreWorldPosition();
        isMovedThisTick = true;
    }

    public bool IsMovedThisTick()
    {
        return isMovedThisTick;
    }

    public abstract Vector2Int GetSize();
    public abstract void Destroy();
}
