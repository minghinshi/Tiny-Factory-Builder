using System.Collections.Generic;
using UnityEngine;

public class Machine : Building
{
    private int ticksLeft = 500;
    private bool isProcessing = false;

    private Inventory inputInventory;
    private Inventory outputInventory;

    private Cell<Item>[] inputCells;
    private Cell<Item> outputCell;
    private Recipe currentRecipe;

    //Creates a machine object.
    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        inputInventory = new Inventory();
        SetInputCells(machineType.GetInputPositions());
        SetOutputCell(machineType.GetOutputPosition());
        currentRecipe = machineType.GetRecipes()[0];
        TickHandler.instance.TickMachines += OnTick;
    }

    private void SetInputCells(Vector2Int[] inputPositions)
    {
        inputCells = new Cell<Item>[inputPositions.Length];
        for (int i = 0; i < inputPositions.Length; i++)
        {
            Vector2Int position = GetGridPositionFromOffset(inputPositions[i]);
            inputCells[i] = GridManager.itemGrid.GetCellAt(position);
            inputCells[i].CellOccupied += StoreInput;
            inputCells[i].UnblockCell();
        }
    }

    private void SetOutputCell(Vector2Int outputPosition)
    {
        outputCell = GridManager.itemGrid.GetCellAt(GetGridPositionFromOffset(outputPosition));
    }

    //Puts an item delivered to the input cell to the storage.
    private void StoreInput(Cell<Item> itemCell)
    {
        ItemType item = itemCell.GetContainedObject().GetItemType();
        inputInventory.StoreOne(item);
        itemCell.DestroyCellObject();
        if (CanProcess()) StartProcess();
    }

    //Returns if the machine can process a recipe with the stored items.
    public bool CanProcess()
    {
        Dictionary<ItemType, int> inputs = currentRecipe.GetInputs();
        foreach (ItemType item in inputs.Keys)
            if (inputInventory.GetItemCountOf(item) < inputs[item]) return false;
        return true;
    }

    public void StartProcess()
    {
        isProcessing = true;
        ConsumeInputs();
    }

    public void ConsumeInputs()
    {
        Dictionary<ItemType, int> inputs = currentRecipe.GetInputs();
        foreach (ItemType item in inputs.Keys)
        {
            bool enoughItems = inputInventory.Remove(item, inputs[item]);
            if (!enoughItems) throw new System.Exception("Attempted to consume items while there is not enough of them!");
        }
    }

    private void OnTick()
    {
        if (isProcessing)
        {
            ticksLeft--;
            if (ticksLeft <= 0)
            {
                ticksLeft = 500;
            }
        }
    }
}
