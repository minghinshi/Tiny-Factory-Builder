using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Machine", menuName = "Machine")]
public class MachineType : ProducerType
{
    [SerializeField] private List<Recipe> recipes;

    public override Building CreateBuilding(Vector2Int gridPosition, Direction direction)
    {
        return new Machine(gridPosition, direction, this);
    }

    public List<Recipe> GetRecipes()
    {
        if (recipes.Count == 0) Debug.LogWarning("There are no recipes for " + name + ". Please add at least 1 recipe.");
        return recipes;
    }

    public bool CanDo(Recipe recipe)
    {
        return recipes.Contains(recipe);
    }
}
