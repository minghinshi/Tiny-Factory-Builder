using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Machine", menuName = "Machine")]
public class MachineType : ProducerType
{
    [SerializeField] private List<Vector2Int> inputPositions;
    [SerializeField] private List<Recipe> recipes;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Machine(gridPosition, direction, this);
    }

    public List<Vector2Int> GetInputPositions()
    {
        return inputPositions;
    }

    public List<Recipe> GetRecipes()
    {
        if (recipes.Count == 0) Debug.LogWarning("There are no recipes for " + name + ". Please add at least 1 recipe.");
        return recipes;
    }
}
