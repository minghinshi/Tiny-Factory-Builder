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
        Craft(GetSingleInput(), RollOutput(GetAverageSingleOutput()));
    }

    public void BatchCraft()
    {
        Craft(GetBatchInput(), RollOutput(GetAverageBatchOutput()));
    }

    public bool CanCraft()
    {
        return GetSingleInput().TrueForAll(x => input.Contains(x));
    }

    public List<ItemStack> GetSingleInput()
    {
        return recipe.GetInputs();
    }

    public List<ICountableItem> GetAverageSingleOutput()
    {
        return recipe.GetAverageOutputs();
    }

    public List<ItemStack> GetBatchInput()
    {
        return GetBatchItems(GetSingleInput(), GetMaximumCrafts());
    }

    public List<ICountableItem> GetAverageBatchOutput()
    {
        return GetBatchItems(GetAverageSingleOutput(), GetMaximumCrafts());
    }

    public bool IsMissing(ItemType itemType)
    {
        return GetMissingItems().Contains(itemType);
    }

    public bool IsLimiting(ItemType itemType)
    {
        return GetLimitingItems().Contains(itemType);
    }

    private List<ItemType> GetMissingItems()
    {
        return GetInsufficientItems(GetSingleInput());
    }

    private List<ItemType> GetLimitingItems()
    {
        return GetInsufficientItems(GetNextBatchInput());
    }

    private int GetMaximumCrafts()
    {
        return GetSingleInput().ConvertAll(x => input.GetItemCount(x.GetItemType()) / x.GetCount()).Min();
    }

    private List<ItemStack> RollOutput(List<ICountableItem> recipeOutputs)
    {
        return recipeOutputs.ConvertAll(x => x.GetItemStack()).FindAll(x => !x.IsEmpty());
    }

    private void Craft(List<ItemStack> inputs, List<ItemStack> outputs)
    {
        outputs.ForEach(x => output.StoreStack(x));
        inputs.ForEach(x => input.RemoveStack(x));
    }

    private List<ItemStack> GetNextBatchInput()
    {
        return GetBatchItems(GetSingleInput(), GetMaximumCrafts() + 1);
    }

    private List<T> GetBatchItems<T>(List<T> original, int multiplier) where T : ICountableItem
    {
        return original.ConvertAll(x => (T)x.MultiplyBy(multiplier));
    }

    private List<ItemType> GetInsufficientItems(List<ItemStack> cost)
    {
        return cost.FindAll(x => !input.Contains(x)).ConvertAll(x => x.GetItemType());
    }
}
