using UnityEngine;

[CreateAssetMenu(fileName = "New Producer", menuName = "Producer")]
public class ProducerType : BuildingType
{
    [SerializeField] private Vector2Int outputPosition;
    //TEMPORARY
    public ItemType producedItem;

    public override Building CreateBuilding(Cell primaryCell, Direction direction)
    {
        return new Producer(primaryCell, direction, this);
    }

    public Vector2Int GetOutputPosition()
    {
        return outputPosition;
    }
}
