using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public abstract class Producer : Building
{
    [JsonProperty] protected Inventory outputInventory = new();
    protected Timer timer;
    private ProducerVisual visual;

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
        if (outputInventory.HasItems()) GiveOutputsToPlayer();
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

    public Timer GetTimer()
    {
        return timer;
    }

    protected abstract Timer GetNewTimer();
    protected abstract void ProduceOutputs();

    protected override void CreateVisuals()
    {
        visual = ProducerVisual.Create(this);
    }

    protected override BuildingVisual GetVisuals()
    {
        return visual;
    }

    private void AddProgressBar()
    {
        Transform progressBar = Object.Instantiate(PrefabLoader.progressBar, visual.transform);
        timer.SetSlider(progressBar.GetComponent<Slider>());
    }

    private void OnTimerEnded()
    {
        timer.Reset();
        ProduceOutputs();
    }

    private void GiveOutputsToPlayer()
    {
        outputInventory.TransferTo(Inventory.playerInventory);
        AudioHandler.instance.PlaySound(AudioHandler.instance.pickUpSound);
    }
}
