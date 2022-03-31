using UnityEngine;

[CreateAssetMenu(fileName = "New Conveyor", menuName = "Conveyor")]
public class ConveyorType : BuildingType
{
    [SerializeField] private Vector2Int[] outputPositions;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Conveyor(gridPosition, direction, this);
    }

    public Vector2Int[] GetOutputPositions()
    {
        return outputPositions;
    }
}
