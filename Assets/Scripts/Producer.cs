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
        outputCell = GridManager.itemGrid.GetCellAt(primaryCell.GetGridPosition() + direction.GetUnitVector());
    }

    /// <summary>
    /// Called when the producer receives a tick.
    /// </summary>
    public void TickProducer()
    {
        ticksLeft--;
        if (ticksLeft == 0)
        {
            ticksLeft = 50;
            ProduceItem();
        }
    }

    /// <summary>
    /// Produces an item, if possible.
    /// </summary>
    public void ProduceItem()
    {
        if (outputCell.CanInsert()) {
            Item product = new Item(outputCell, producedItem);
        }
    }
}
