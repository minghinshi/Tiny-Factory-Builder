using UnityEngine;

public class Producer : Building
{
    private Cell<Item> outputCell;
    private ItemType producedItem;
    private int ticksLeft = 50;

    //Creates a producer (e.g. drill).
    public Producer(Vector2Int gridPosition, Direction direction, ProducerType producerType) : base(gridPosition, direction, producerType)
    {
        //TEMPORARY
        //Later on, the producer will check for the resource type below it.
        producedItem = producerType.producedItem;
        Vector2Int outputPosition = producerType.GetOutputPosition();
        outputCell = AllGrids.itemGrid.GetCellAt(GetGridPositionFromOffset(outputPosition));

        TickHandler.instance.TickMachines += OnTick;
    }

    //Called when the producer receives a tick.
    private void OnTick()
    {
        ticksLeft--;
        if (ticksLeft <= 0)
        {
            ProduceItem();
        }
    }

    //Produces an item, if possible.
    public void ProduceItem()
    {
        if (outputCell.CanInsert())
        {
            _ = new Item(outputCell, producedItem);
            ticksLeft = 50;
        }
    }

    //Destroys the producer, removing it from the world.
    public override void Destroy()
    {
        TickHandler.instance.TickMachines -= OnTick;

        base.Destroy();
    }
}
