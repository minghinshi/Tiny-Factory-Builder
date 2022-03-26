using UnityEngine;

[CreateAssetMenu(fileName = "New Producer", menuName = "Producer")]
public class ProducerType : BuildingType
{
    [SerializeField] private Vector2Int outputPosition;
    //TEMPORARY
    public ItemType producedItem;
}
