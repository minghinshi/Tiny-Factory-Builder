using System.Collections.Generic;
using System.Linq;

public class CraftingRequest
{
    private Recipe recipe;
    private Inventory inputInventory;

    public CraftingRequest(Recipe recipe, Inventory inputInventory)
    {
        this.recipe = recipe;
        this.inputInventory = inputInventory;
    }

    public int GetMaximumCrafts()
    {
        return GetSingleInput().ConvertAll(x => inputInventory.GetItemCount(x.GetItemType()) / x.GetCount()).Min();
    }

    public bool CanCraft()
    {
        return GetSingleInput().TrueForAll(x => inputInventory.Contains(x));
    }

    public List<ItemStack> GetSingleInput()
    {
        return recipe.GetInputs();
    }

    public List<ItemStack> GetSingleOutput()
    {
        return recipe.GetOutputs();
    }

    public List<ItemStack> GetBatchInput()
    {
        return GetBatchItemStacks(GetSingleInput(), GetMaximumCrafts());
    }

    public List<ItemStack> GetBatchOutput()
    {
        return GetBatchItemStacks(GetSingleOutput(), GetMaximumCrafts());
    }

    public List<ItemType> GetMissingItems()
    {
        return GetInsufficientItems(GetSingleInput());
    }

    public List<ItemType> GetLimitingItems()
    {
        return GetInsufficientItems(GetNextBatchInput());
    }

    private List<ItemStack> GetNextBatchInput()
    {
        return GetBatchItemStacks(GetSingleInput(), GetMaximumCrafts() + 1);
    }

    private List<ItemStack> GetBatchItemStacks(List<ItemStack> original, int multiplier)
    {
        return original.ConvertAll(x => x.GetCopies(multiplier));
    }

    private List<ItemType> GetInsufficientItems(List<ItemStack> cost)
    {
        return cost.FindAll(x => inputInventory.Contains(x)).ConvertAll(x => x.GetItemType());
    }
}
