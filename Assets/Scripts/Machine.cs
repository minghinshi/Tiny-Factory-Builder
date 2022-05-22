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
            inputCells[i] = AllGrids.itemGrid.GetCellAt(position);
            inputCells[i].CellOccupied += StoreInput;
            inputCells[i].UnblockCell();
        }
    }

    private void SetOutputCell(Vector2Int outputPosition)
    {
        outputCell = AllGrids.itemGrid.GetCellAt(GetGridPositionFromOffset(outputPosition));
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
        ItemStack[] inputs = currentRecipe.GetInputs();
        foreach (ItemStack input in inputs)
            if (inputInventory.GetItemCount(input.GetItemType()) < input.GetCount()) return false;
        return true;
    }

    public void StartProcess()
    {
        isProcessing = true;
        ConsumeInputs();
    }

    public void ConsumeInputs()
    {
        ItemStack[] inputs = currentRecipe.GetInputs();
        foreach (ItemStack input in inputs)
            inputInventory.RemoveCopyOf(input);
    }

    private void OnTick()
    {
        if (isProcessing) TickRecipe();
    }

    private void TickRecipe()
    {
        ticksLeft--;
        if (ticksLeft <= 0) CompleteRecipe();
    }

    private void CompleteRecipe()
    {
        ticksLeft = 500;
    }
}
