using System;
using UnityEngine;

[Serializable]
public class Machine : Producer
{
    [SerializeField] private Inventory inputInventory = new Inventory();

    private Process currentProcess;
    private readonly MachineType machineType;

    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        this.machineType = machineType;
        inputInventory.Updated += OnInputInventoryUpdated;
    }

    public override void OnClick()
    {
        base.OnClick();
        if (Input.GetKey(KeyCode.LeftShift)) inputInventory.TransferTo(Inventory.playerInventory);
    }

    public override void Destroy()
    {
        inputInventory.TransferTo(Inventory.playerInventory);
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
            Process craftingRequest = new Process(recipe, inputInventory, outputInventory);
            if (craftingRequest.CanCraft()) return craftingRequest;
        }
        return null;
    }
}