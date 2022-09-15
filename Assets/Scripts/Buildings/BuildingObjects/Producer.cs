using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public abstract class Producer : Building
{
    [JsonProperty] protected Inventory outputInventory = new();

    protected Timer timer;

    protected Producer(Vector2Int gridPosition, Direction direction) : base(gridPosition, direction) { }

    public override void Initialize()
    {
        base.Initialize();
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
