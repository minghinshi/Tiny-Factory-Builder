using Newtonsoft.Json;
using UnityEngine;

public class Machine : Producer
{
    [JsonProperty] private readonly MachineType machineType;
    [JsonProperty] private readonly Inventory inputInventory = new();

    private Process currentProcess;

    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction)
    {
        this.machineType = machineType;
    }

    public override void OnClick()
    {
        base.OnClick();
        if (Input.GetKey(KeyCode.LeftShift) && inputInventory.HasItems()) GiveInputsToPlayer();
    }

    public override void Destroy()
    {
        inputInventory.TransferTo(PlayerInventory.instance);
        inputInventory.Changed -= OnInputInventoryUpdated;
        base.Destroy();
    }

    public override BuildingType GetBuildingType()
    {
        return machineType;
    }

    public override bool CanInsert() => true;
    public override void Insert(ItemStack itemStack) => inputInventory.StoreStack(itemStack);

    public Inventory GetInputInventory()
    {
        return inputInventory;
    }

    protected override void InitializeData()
    {
        base.InitializeData();
        OnInputInventoryUpdated();
        inputInventory.Changed += OnInputInventoryUpdated;
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(150, false);
    }

    protected override void ProduceOutputs()
    {
        currentProcess.CraftOnce();
    }

    private void OnInputInventoryUpdated()
    {
        if (IsRunning() && CanProcess()) return;
        StartNewProcess();
    }

    private void StartNewProcess()
    {
        timer.Reset();
        currentProcess = FindCraftableProcess();
        if (IsRunning()) timer.Resume();
        else timer.Pause();
    }

    private bool CanProcess()
    {
        return currentProcess.CanCraft();
    }

    private bool IsRunning()
    {
        return currentProcess != null;
    }

    private Process FindCraftableProcess()
    {
        foreach (Recipe recipe in machineType.GetRecipes())
        {
            Process craftingRequest = new(recipe, inputInventory, outputInventory);
            if (craftingRequest.CanCraft()) return craftingRequest;
        }
        return null;
    }

    private void GiveInputsToPlayer()
    {
        inputInventory.TransferTo(PlayerInventory.instance);
        AudioHandler.instance.PlaySound(AudioHandler.instance.pickUpSound);
    }
}