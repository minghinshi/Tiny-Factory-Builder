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

    public bool CanCraft(Inventory inventory)
    {
        return inputs.TrueForAll(x => inventory.GetItemCount(x.GetItemType()) >= x.GetCount());
    }

    public void CraftOnce(Inventory inputInventory, Inventory outputInventory)
    {
        inputs.ForEach(x => inputInventory.RemoveCopyOf(x));
        outputs.ForEach(x => outputInventory.StoreCopyOf(x));
    }
}