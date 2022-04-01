using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] public Dictionary<ItemType, int> inputs;
    [SerializeField] private Dictionary<ItemType, int> outputs;

    public Dictionary<ItemType, int> GetInputs()
    {
        return inputs;
    }
}
