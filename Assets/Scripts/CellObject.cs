using UnityEngine;

public abstract class CellObject
{
    protected Vector2Int gridPosition;
    protected Transform transform;
    protected bool isMovedThisTick;

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
        isMovedThisTick = true;
    }

    public bool IsMovedThisTick()
    {
        return isMovedThisTick;
    }

    public abstract Vector2Int GetSize();
    public abstract void Destroy();
}
