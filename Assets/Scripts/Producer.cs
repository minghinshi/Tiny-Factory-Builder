using UnityEngine;

public class Producer : Building
{
    private Cell outputCell;
    private ItemType producedItem;
    private int ticksLeft = 50;

    public Producer(Cell primaryCell, Direction direction, ProducerType producerType) : base(primaryCell, direction, producerType)
    {
        //TEMPORARY
        //Later on, the producer will check for the resource type below it.
        producedItem = producerType.producedItem;
        Vector2Int outputPosition = producerType.GetOutputPosition();
        Vector2Int gridPosition = GetGridPositionFromOffset(outputPosition);
        outputCell = GridManager.itemGrid.GetCellAt(gridPosition);

        TickHandler.instance.TickMachines += TickProducer;
    }

    /// <summary>
    /// Called when the producer receives a tick.
    /// </summary>
    public void TickProducer()
    {
        ticksLeft--;
        if (ticksLeft <= 0)
        {
            ProduceItem();
        }
    }

    /// <summary>
    /// Produces an item, if possible.
    /// </summary>
    public void ProduceItem()
    {
        if (outputCell.CanInsert()) {
            _ = new Item(outputCell, producedItem);
            ticksLeft = 50;
        }
    }

    public override void Destroy()
    {
        TickHandler.instance.TickMachines -= TickProducer;

        base.Destroy();
    }
}
