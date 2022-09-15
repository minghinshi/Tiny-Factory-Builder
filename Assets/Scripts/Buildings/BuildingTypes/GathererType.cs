using UnityEngine;

[CreateAssetMenu(fileName = "New Gatherer", menuName = "Gatherer")]
public class GathererType : BuildingType
{
    [SerializeField] private ItemType producedItem;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Gatherer(gridPosition, direction, this);
    }

    public ItemType GetProducedItem() => producedItem;
}
