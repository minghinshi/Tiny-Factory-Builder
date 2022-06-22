using UnityEngine;

public abstract class ProducerType : BuildingType
{
    [SerializeField] private Vector2Int outputPosition;

    public Vector2Int GetOutputPosition()
    {
        return outputPosition;
    }
}
