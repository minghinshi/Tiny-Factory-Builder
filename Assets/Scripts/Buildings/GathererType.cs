using UnityEngine;

[CreateAssetMenu(fileName = "New Gatherer", menuName = "Gatherer")]
public class GathererType : ProducerType
{
    //TEMPORARY
    public ItemType producedItem;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Gatherer(gridPosition, direction, this);
    }
}
