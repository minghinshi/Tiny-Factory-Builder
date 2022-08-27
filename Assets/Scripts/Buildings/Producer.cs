using UnityEngine;
using UnityEngine.UI;

public abstract class Producer : Building
{
    [SerializeField] protected Inventory outputInventory = new Inventory();

    protected Timer timer;

    public Producer(Vector2Int gridPosition, Direction direction, BuildingType buildingType) : base(gridPosition, direction, buildingType)
    {
        timer = GetNewTimer();
        timer.TimerEnded += OnTimerEnded;
        AddProgressBar();
    }

    public override void Destroy()
    {
        timer.Destroy();
        timer.TimerEnded -= OnTimerEnded;
        outputInventory.TransferTo(Inventory.playerInventory);
        base.Destroy();
    }

    public override void OnClick()
    {
        outputInventory.TransferTo(Inventory.playerInventory);
    }

    public override bool CanExtract() => outputInventory.HasItems();

    public override ItemStack Extract()
    {
        ItemStack itemStack = new ItemStack(outputInventory.GetFirstItemType(), 1);
        outputInventory.RemoveCopyOf(itemStack);
        return itemStack;
    }

    public void AddProgressBar()
    {
        Transform progressBar = Object.Instantiate(PrefabLoader.progressBar, transform);
        timer.SetSlider(progressBar.GetComponent<Slider>());
    }

    public Inventory GetOutputInventory()
    {
        return outputInventory;
    }

    protected abstract Timer GetNewTimer();
    protected abstract void ProduceOutputs();

    private void OnTimerEnded()
    {
        timer.Reset();
        ProduceOutputs();
    }
}
