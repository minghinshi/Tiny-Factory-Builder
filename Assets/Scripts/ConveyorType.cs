using UnityEngine;

[CreateAssetMenu(fileName = "New Conveyor", menuName = "Conveyor")]
public class ConveyorType : BuildingType
{
    [SerializeField] private Vector2Int[] outputPositions;

    public override Building CreateBuilding(Cell primaryCell, Direction direction)
    {
        return new Conveyor(primaryCell, direction, this);
    }

    public Vector2Int[] GetOutputPositions()
    {
        return outputPositions;
    }
}
