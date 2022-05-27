using UnityEngine;
using System.Collections.Generic;

public class Machine : Producer
{
    private Inventory inputInventory = new Inventory();
    private Inventory outputInventory = new Inventory();

    private List<Cell<Item>> inputCells = new List<Cell<Item>>();
    private Recipe currentRecipe;

    //Creates a machine object.
    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        SetInputCells(machineType.GetInputPositions());
        ConnectInputCells();
        currentRecipe = machineType.GetRecipes()[0];
        TickHandler.instance.TickMachines += ProduceOutputs;
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(500, false);
    }

    protected override void OnTimerEnded()
    {
        timer.Reset();
        AddOutputs();
        if (CanProcess()) ConsumeInputs();
        else timer.Pause();
    }

    public override void Destroy()
    {
        TickHandler.instance.TickMachines -= ProduceOutputs;
        DisconnectInputCells();
        base.Destroy();
    }

    private void SetInputCells(List<Vector2Int> relativePositions)
    {
        inputCells = RelativePositionsToCells(relativePositions);
    }

    private void ConnectInputCells() {
        inputCells.ForEach(x => ConnectInputCell(x));
    }

    private void ConnectInputCell(Cell<Item> inputCell) {
        inputCell.CellOccupied += OnInputCellOccupied;
        inputCell.UnblockCell();
    }

    private void DisconnectInputCells() {
        inputCells.ForEach(x => DisconnectInputCell(x));
    }

    private void DisconnectInputCell(Cell<Item> inputCell) {
        inputCell.CellOccupied -= OnInputCellOccupied;
        inputCell.BlockCell();
    }

    private void OnInputCellOccupied(Cell<Item> itemCell)
    {
        StoreInput(itemCell);
        if (!timer.IsEnabled() && CanProcess()) StartProcess();
    }

    //Puts an item delivered to the input cell to the storage.
    private void StoreInput(Cell<Item> itemCell)
    {
        ItemType item = itemCell.GetContainedObject().GetItemType();
        inputInventory.Store(item, 1);
        itemCell.DestroyCellObject();
    }

    //Returns if the machine can process a recipe with the stored items.
    private bool CanProcess()
    {
        ItemStack[] inputs = currentRecipe.GetInputs();
        foreach (ItemStack input in inputs)
            if (inputInventory.GetItemCount(input.GetItemType()) < input.GetCount()) return false;
        return true;
    }

    private void StartProcess()
    {
        timer.Resume();
        ConsumeInputs();
    }

    private void ConsumeInputs()
    {
        ItemStack[] inputs = currentRecipe.GetInputs();
        foreach (ItemStack input in inputs)
            inputInventory.RemoveCopyOf(input);
    }

    private void AddOutputs()
    {
        foreach (ItemStack output in currentRecipe.GetOutputs())
            outputInventory.StoreCopyOf(output);
    }

    private void ProduceOutputs()
    {
        if (outputCell.CanInsert() && outputInventory.HasItems())
        {
            ItemType producedItem = outputInventory.GetFirstItemType();
            outputInventory.Remove(producedItem, 1);
            ProduceItem(producedItem);
        }
    }
}