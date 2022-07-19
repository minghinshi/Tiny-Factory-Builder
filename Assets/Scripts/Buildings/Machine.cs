using UnityEngine;

public class Machine : Producer
{
    private readonly MachineType machineType;
    private Inventory inputInventory = new Inventory();
    private Recipe currentRecipe;

    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        this.machineType = machineType;
        inputInventory.Updated += OnInputInventoryUpdated;
    }

    public override void OnClick()
    {
        base.OnClick();
        if (Input.GetKey(KeyCode.LeftShift)) inputInventory.TransferTo(PlayerInventory.inventory);
    }

    public override void Destroy()
    {
        inputInventory.TransferTo(PlayerInventory.inventory);
        base.Destroy();
    }

    public override bool CanInsert() => true;
    public override void Insert(ItemStack itemStack) => inputInventory.StoreCopyOf(itemStack);

    public Inventory GetInputInventory()
    {
        return inputInventory;
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(150, false);
    }

    protected override void StoreOutputs()
    {
        currentRecipe.CraftOnce(inputInventory, outputInventory);
    }

    private void OnInputInventoryUpdated()
    {
        if (IsRunning() && CanProcess()) return;
        StartNewRecipe();
    }

    private void StartNewRecipe()
    {
        timer.Reset();
        currentRecipe = GetCraftableRecipe();
        if (currentRecipe == null) timer.Pause();
        else timer.Resume();
    }

    private bool CanProcess()
    {
        return currentRecipe.CanCraft(inputInventory);
    }

    private bool IsRunning()
    {
        return currentRecipe != null;
    }

    private Recipe GetCraftableRecipe()
    {
        foreach (Recipe recipe in machineType.GetRecipes())
            if (recipe.CanCraft(inputInventory)) return recipe;
        return null;
    }
}