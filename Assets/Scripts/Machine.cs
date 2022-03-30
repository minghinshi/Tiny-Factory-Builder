using System.Collections.Generic;
using UnityEngine;

public class Machine : Building
{
    private int ticksLeft = 500;
    private bool isProcessing = false;

    private Dictionary<string, int> storedInputsItems;

    private Cell[] inputCells;
    private Cell outputCell;
    private Recipe currentRecipe;

    //Creates a machine object.
    public Machine(Cell primaryCell, Direction direction, MachineType machineType) : base(primaryCell, direction, machineType)
    {
        Vector2Int[] inputPositions = machineType.GetInputPositions();
        for (int i = 0; i < inputPositions.Length; i++)
        {
            Vector2Int position = GetGridPositionFromOffset(inputPositions[i]);
            inputCells[i] = GridManager.itemGrid.GetCellAt(position);
            inputCells[i].CellOccupied += StoreInput;
        }

        Vector2Int outputPosition = machineType.GetOutputPosition();
        outputCell = GridManager.itemGrid.GetCellAt(GetGridPositionFromOffset(outputPosition));

        TickHandler.instance.TickMachines += OnTick;
    }

    //Puts an item delivered to the input cell to the storage.
    private void StoreInput(Cell cell)
    {

    }

    //Returns if the machine can process a recipe with the stored items.
    public bool CanProcess()
    {
        Dictionary<string, int> inputs = currentRecipe.GetInputs();
        foreach (string item in inputs.Keys)
        {
            if (storedInputsItems[item] < inputs[item]) return false;
        }
        return true;
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
