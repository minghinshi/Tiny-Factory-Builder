using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private ItemStack[] inputs;
    [SerializeField] private ItemStack[] outputs;

    public ItemStack[] GetInputs() {
        return inputs;
    }
}
