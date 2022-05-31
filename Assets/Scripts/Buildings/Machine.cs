using System.Collections.Generic;
using UnityEngine;

public class Machine : Producer
{
    private Inventory inputInventory = new Inventory();

    private List<Cell<Item>> inputCells = new List<Cell<Item>>();
    private Recipe currentRecipe;

    //Creates a machine object.
    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        SetInputCells(machineType.GetInputPositions());
        ConnectInputCells();
        currentRecipe = machineType.GetRecipes()[0];
        inputInventory.Updated += OnInputInventoryUpdated;
    }

    public override void Destroy()
    {
        DisconnectInputCells();
        inputInventory.TransferTo(PlayerInventory.inventory);
        base.Destroy();
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
        if (CanProcess())
            timer.Resume();
        else
        {
            timer.Pause();
            timer.Reset();
        }
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

    private bool CanProcess()
    {
        return currentRecipe.CanCraft(inputInventory);
    }
}