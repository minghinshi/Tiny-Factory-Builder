using UnityEngine;

public abstract class PlacedBuildingVisual : BuildingVisual
{
    private Building building;

    protected void Initialize(Building building)
    {
        this.building = building;
        Initialize();
    }

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
}