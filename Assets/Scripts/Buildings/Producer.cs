using UnityEngine;

public abstract class Producer : Building
{
    protected Timer timer;
    protected Inventory outputInventory = new Inventory();

    public Producer(Vector2Int gridPosition, Direction direction, ProducerType producerType) : base(gridPosition, direction, producerType)
    {
        timer = GetNewTimer();
        timer.TimerEnded += OnTimerEnded;
    }

    public override void Destroy()
    {
        timer.Destroy();
        timer.TimerEnded -= OnTimerEnded;
        outputInventory.TransferTo(PlayerInventory.inventory);
        base.Destroy();
    }

    public override void OnClick()
    {
        outputInventory.TransferTo(PlayerInventory.inventory);
    }

    public override bool CanExtract() => outputInventory.HasItems();

    public override ItemStack Extract()
    {
        ItemStack itemStack = new ItemStack(outputInventory.GetFirstItemType(), 1);
        outputInventory.RemoveCopyOf(itemStack);
        return itemStack;
    }

    public Inventory GetOutputInventory()
    {
        return outputInventory;
    }

    protected abstract Timer GetNewTimer();

    protected abstract void StoreOutputs();

    private void OnTimerEnded()
    {
        timer.Reset();
        StoreOutputs();
    }
}
