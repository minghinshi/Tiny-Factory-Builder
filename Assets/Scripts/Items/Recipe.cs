using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject, IDisplayableAsItem
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

    public bool Produces(ItemType itemType)
    {
        foreach (ItemStack itemStack in outputs) if (itemStack.GetItemType().Equals(itemType)) return true;
        return false;
    }

    public bool CanCraft(Inventory inventory)
    {
        return inputs.TrueForAll(x => inventory.Contains(x));
    }

    public int GetMaximumCrafts(Inventory inventory)
    {
        return Mathf.Min(inputs.ConvertAll(x => inventory.GetItemCount(x.GetItemType()) / x.GetCount()).ToArray());
    }

    public void CraftOnce(Inventory inputInventory, Inventory outputInventory)
    {
        Craft(inputInventory, outputInventory, 1);
    }

    public void CraftAll(Inventory inputInventory, Inventory outputInventory)
    {
        Craft(inputInventory, outputInventory, GetMaximumCrafts(inputInventory));
    }

    private void Craft(Inventory inputInventory, Inventory outputInventory, int count)
    {
        inputs.ForEach(x => inputInventory.RemoveCopiesOf(x, count));
        outputs.ForEach(x => outputInventory.StoreCopiesOf(x, count));
    }
}
