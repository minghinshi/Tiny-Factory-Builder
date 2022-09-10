using System.Collections.Generic;
using System.Linq;

public class Process
{
    private readonly Recipe recipe;
    private readonly Inventory input;
    private readonly Inventory output;

    public Process(Recipe recipe, Inventory input, Inventory output)
    {
        this.recipe = recipe;
        this.input = input;
        this.output = output;
    }

    public void CraftOnce()
    {
        Craft(GetSingleInput(), GetSingleOutput());
    }

    public void BatchCraft()
    {
        Craft(GetBatchInput(), GetBatchOutput());
    }

    public bool CanCraft()
    {
        return GetSingleInput().TrueForAll(x => input.Contains(x));
    }

    public List<ItemStack> GetSingleInput()
    {
        return recipe.GetInputs();
    }

    public List<IRecipeOutput> GetAverageSingleOutput()
    {
        return recipe.GetAverageOutputs();
    }

    public List<ItemStack> GetBatchInput()
    {
        return GetBatchItems(GetSingleInput(), GetMaximumCrafts());
    }

    public List<IRecipeOutput> GetAverageBatchOutput()
    {
        return GetBatchItems(GetAverageSingleOutput(), GetMaximumCrafts());
    }

    public List<ItemType> GetMissingItems()
    {
        return GetInsufficientItems(GetSingleInput());
    }

    public List<ItemType> GetLimitingItems()
    {
        return GetInsufficientItems(GetNextBatchInput());
    }

    private int GetMaximumCrafts()
    {
        return GetSingleInput().ConvertAll(x => input.GetItemCount(x.GetItemType()) / x.GetCount()).Min();
    }

    private List<ItemStack> GetSingleOutput()
    {
        return GetAverageSingleOutput().ConvertAll(x => x.GetItemStack());
    }

    private List<ItemStack> GetBatchOutput()
    {
        return GetAverageBatchOutput().ConvertAll(x => x.GetItemStack());
    }

    private void Craft(List<ItemStack> inputs, List<ItemStack> outputs)
    {
        outputs.ForEach(x => output.StoreCopyOf(x));
        inputs.ForEach(x => input.RemoveCopyOf(x));
    }

    private List<ItemStack> GetNextBatchInput()
    {
        return GetBatchItems(GetSingleInput(), GetMaximumCrafts() + 1);
    }

    private List<T> GetBatchItems<T>(List<T> original, int multiplier) where T : IRecipeOutput
    {
        return original.ConvertAll(x => (T)x.MultiplyBy(multiplier));
    }

    private List<ItemType> GetInsufficientItems(List<ItemStack> cost)
    {
        return cost.FindAll(x => !input.Contains(x)).ConvertAll(x => x.GetItemType());
    }
}
