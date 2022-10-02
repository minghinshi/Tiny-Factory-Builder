using UnityEngine;

public abstract class PlacedVisual : BuildingVisual
{
    private Building building;

    protected override BuildingType GetBuildingType()
    {
        return building.GetBuildingType();
    }

    protected override Direction GetDirection()
    {
        return building.GetDirection();
    }

    protected override Vector2Int GetGridPosition()
    {
        return building.GetGridPosition();
    }

    protected void Initialize(Building building)
    {
        this.building = building;
        Initialize();
    }
}