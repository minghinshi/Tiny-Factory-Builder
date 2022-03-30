using UnityEngine;

[CreateAssetMenu(fileName = "New Machine", menuName = "Machine")]
public class MachineType : BuildingType
{
    private Vector2Int[] inputPositions;
    private Vector2Int outputPosition;

    private Recipe[] recipes;

    public Vector2Int[] GetInputPositions()
    {
        return inputPositions;
    }

    public Vector2Int GetOutputPosition()
    {
        return outputPosition;
    }

    public Recipe[] GetRecipes()
    {
        return recipes;
    }
}
