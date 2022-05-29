using System.Collections.Generic;
using UnityEngine;

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
        inputInventory.Updated += OnInputInventoryUpdated;
    }

    private void OnInputInventoryUpdated()
    {
        if (CanProcess())
            timer.Resume();
        else
        {
            timer.Pause();
            timer.Reset();
        }
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(500, false);
    }

    protected override void OnTimerEnded()
    {
        currentRecipe.CraftOnce(inputInventory, outputInventory);
        timer.Reset();
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

    private void ConnectInputCells()
    {
        inputCells.ForEach(x => ConnectInputCell(x));
    }

    private void ConnectInputCell(Cell<Item> inputCell)
    {
        inputCell.CellOccupied += OnInputCellOccupied;
        inputCell.UnblockCell();
    }

    private void DisconnectInputCells()
    {
        inputCells.ForEach(x => DisconnectInputCell(x));
    }

    private void DisconnectInputCell(Cell<Item> inputCell)
    {
        inputCell.CellOccupied -= OnInputCellOccupied;
        inputCell.BlockCell();
    }

    private void OnInputCellOccupied(Cell<Item> itemCell)
    {
        StoreInput(itemCell);
    }

    private void StoreInput(Cell<Item> itemCell)
    {
        ItemType item = itemCell.GetContainedObject().GetItemType();
        inputInventory.Store(item, 1);
        itemCell.DestroyCellObject();
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

    private bool CanProcess()
    {
        return currentRecipe.CanCraft(inputInventory);
    }
}