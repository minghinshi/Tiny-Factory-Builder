using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<ItemStack> inputs;
    [SerializeField] private List<ItemStack> outputs;

    public List<ItemStack> GetInputs()
    {
        return inputs;
    }

    public List<ItemStack> GetOutputs()
    {
        return outputs;
    }

    public List<MachineType> GetMachines() { 
        return DataLoader.allMachines.FindAll(x => x.CanDo(this));
    }

    public bool Produces(ItemType itemType)
    {
        foreach (ItemStack itemStack in outputs) if (itemStack.GetItemType().Equals(itemType)) return true;
        return false;
    }
}
