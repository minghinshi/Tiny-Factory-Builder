using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conveyor", menuName = "Conveyor")]
public class ConveyorType : BuildingType
{
    [SerializeField] private List<Vector2Int> inputPositions;
    [SerializeField] private List<Vector2Int> outputPositions;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Conveyor(gridPosition, direction, this);
    }

    public List<Vector2Int> GetInputPositions()
    {
        return inputPositions;
    }

    public List<Vector2Int> GetOutputPositions()
    {
        return outputPositions;
    }
}
