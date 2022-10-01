using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private List<ItemStack> inputs;

    [SerializeReference, SerializeReferenceButton]
    private List<ICountableItem> outputs;

    public List<ItemStack> GetInputs()
    {
        return inputs;
    }

    public List<ICountableItem> GetAverageOutputs()
    {
        return outputs;
    }

    public List<MachineType> GetMachines()
    {
        return GameDataHelper.allMachines.FindAll(x => x.CanDo(this));
    }

    public bool Produces(ItemType itemType)
    {
        return outputs.ConvertAll(x => x.GetItemType()).Contains(itemType);
    }

    public HashSet<ItemType> GetRequiredItems()
    {
        HashSet<ItemType> itemTypes = new();
        itemTypes.UnionWith(inputs.ConvertAll(x => x.GetItemType()));
        itemTypes.UnionWith(outputs.ConvertAll(x => x.GetItemType()));
        itemTypes.UnionWith(GetMachines());
        return itemTypes;
    }
}
