using UnityEngine;

public class Machine : Producer
{
    private Inventory inputInventory = new Inventory();
    private Inventory outputInventory = new Inventory();

    private Cell<Item>[] inputCells;
    private Recipe currentRecipe;

    //Creates a machine object.
    public Machine(Vector2Int gridPosition, Direction direction, MachineType machineType) : base(gridPosition, direction, machineType)
    {
        SetInputCells(machineType.GetInputPositions());
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
        base.Destroy();
    }

    private void SetInputCells(Vector2Int[] inputPositions)
    {
        inputCells = new Cell<Item>[inputPositions.Length];
        for (int i = 0; i < inputPositions.Length; i++)
            SetInputCell(inputPositions[i], inputCells[i]);
    }

    private void SetInputCell(Vector2Int inputPosition, Cell<Item> inputCell) {
        Vector2Int position = GetGridPositionFromOffset(inputPosition);
        inputCell = Grids.itemGrid.GetCellAt(position);
        inputCell.CellOccupied += OnInputCellOccupied;
        inputCell.UnblockCell();
    }

    private void OnInputCellOccupied(Cell<Item> itemCell) {
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

    private void ProduceOutputs() {
        if (outputCell.CanInsert() && outputInventory.HasItems()) {
            ItemType producedItem = outputInventory.GetFirstItemType();
            outputInventory.Remove(producedItem, 1);
            ProduceItem(producedItem);
        }
    }
}