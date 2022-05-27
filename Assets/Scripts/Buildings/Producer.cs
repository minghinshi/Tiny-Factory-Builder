using UnityEngine;

public abstract class Producer : Building
{
    protected Timer timer;
    protected Cell<Item> outputCell;

    public Producer(Vector2Int gridPosition, Direction direction, ProducerType producerType) : base(gridPosition, direction, producerType)
    {
        SetOutputCell(producerType.GetOutputPosition());
        timer = GetNewTimer();
        timer.TimerEnded += OnTimerEnded;
    }

    public override void Destroy()
    {
        timer.Destroy();
        base.Destroy();
    }

    protected abstract Timer GetNewTimer();

    protected abstract void OnTimerEnded();

    protected void ProduceItem(ItemType itemType)
    {
        _ = new Item(outputCell, itemType);
    }

    private void SetOutputCell(Vector2Int outputPosition)
    {
        outputCell = RelativePositionToCell(outputPosition);
    }
}
