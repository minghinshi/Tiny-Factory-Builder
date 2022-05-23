using UnityEngine;

public class Gatherer : Producer
{
    private ItemType producedItem;

    public Gatherer(Vector2Int gridPosition, Direction direction, GathererType gathererType) : base(gridPosition, direction, gathererType)
    {
        producedItem = gathererType.producedItem;

    }

    protected override Timer GetNewTimer()
    {
        return new Timer(50, true);
    }

    protected override void OnTimerEnded()
    {
        if (outputCell.CanInsert()) {
            ProduceItem(producedItem);
            timer.Reset();
        }
    }
}
