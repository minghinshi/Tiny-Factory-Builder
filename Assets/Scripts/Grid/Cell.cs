using UnityEngine;

public class Cell
{
    protected Vector2Int gridPosition;
    protected Building containedBuilding;

    public Cell(Vector2Int gridPosition, Vector3 worldPosition)
    {
        this.gridPosition = gridPosition;
        CentreWorldPosition = worldPosition;
    }

    public Vector3 CentreWorldPosition { get; set; }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public Building GetContainedBuilding()
    {
        return containedBuilding;
    }

    public bool IsOccupied()
    {
        return containedBuilding != null;
    }

    public void OccupyWith(Building building)
    {
        containedBuilding = building;
    }

    public void Empty()
    {
        containedBuilding = null;
    }
}
