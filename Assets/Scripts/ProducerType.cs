using UnityEngine;

[CreateAssetMenu(fileName = "New Producer", menuName = "Producer")]
public class ProducerType : BuildingType
{
    [SerializeField] private Vector2Int outputPosition;
    //TEMPORARY
    public ItemType producedItem;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Producer(gridPosition, direction, this);
    }

    public Vector2Int GetOutputPosition()
    {
        return outputPosition;
    }
}
