using UnityEngine;

public abstract class Producer : Building
{
    protected Timer timer;
    protected Cell<Item> outputCell;
    protected Inventory outputInventory = new Inventory();

    public Producer(Vector2Int gridPosition, Direction direction, ProducerType producerType) : base(gridPosition, direction, producerType)
    {
        SetOutputCell(producerType.GetOutputPosition());
        timer = GetNewTimer();
        timer.TimerEnded += OnTimerEnded;
        TickHandler.instance.TickMachines += ProduceOutputs;
    }

    public override void Destroy()
    {
        timer.Destroy();
        timer.TimerEnded -= OnTimerEnded;
        TickHandler.instance.TickMachines -= ProduceOutputs;
        outputInventory.TransferTo(PlayerInventory.inventory);
        base.Destroy();
    }

    public override void OnClick()
    {
        outputInventory.TransferTo(PlayerInventory.inventory);
    }

    protected abstract Timer GetNewTimer();

    protected abstract void StoreOutputs();

    private void ProduceItem(ItemType itemType)
    {
        _ = new Item(outputCell, itemType);
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

    private void OnTimerEnded()
    {
        timer.Reset();
        StoreOutputs();
    }

    private void SetOutputCell(Vector2Int outputPosition)
    {
        outputCell = RelativePositionToCell(outputPosition);
    }
}
