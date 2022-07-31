using System.Collections.Generic;
using System.Linq;

public class CraftingRequest
{
    private Recipe recipe;
    private Inventory input;
    private Inventory output;

    public CraftingRequest(Recipe recipe, Inventory input, Inventory output)
    {
        this.recipe = recipe;
        this.input = input;
        this.output = output;
    }

    public void SingleCraft()
    {
        Craft(GetSingleInput(), GetSingleOutput());
    }

    public void BatchCraft()
    {
        Craft(GetBatchInput(), GetBatchOutput());
    }

    public int GetMaximumCrafts()
    {
        return GetSingleInput().ConvertAll(x => input.GetItemCount(x.GetItemType()) / x.GetCount()).Min();
    }

    public bool CanCraft()
    {
        return GetSingleInput().TrueForAll(x => input.Contains(x));
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

    private void Craft(List<ItemStack> inputs, List<ItemStack> outputs)
    {
        outputs.ForEach(x => output.StoreCopyOf(x));
        inputs.ForEach(x => input.RemoveCopyOf(x));
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
        return cost.FindAll(x => input.Contains(x)).ConvertAll(x => x.GetItemType());
    }
}
