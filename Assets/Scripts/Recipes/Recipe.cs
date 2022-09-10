using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] 
    private List<ItemStack> inputs;

    [SerializeReference, SerializeReferenceButton] 
    private List<IRecipeOutput> outputs;

    public List<ItemStack> GetInputs()
    {
        return inputs;
    }

    public List<IRecipeOutput> GetAverageOutputs() {
        return outputs;
    }

    public List<MachineType> GetMachines()
    {
        return ScriptableObjectLoader.allMachines.FindAll(x => x.CanDo(this));
    }

    public bool Produces(ItemType itemType)
    {
        return outputs.ConvertAll(x => x.GetItemType()).Contains(itemType);
    }
}
