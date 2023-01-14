using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Building
{
    [JsonProperty] private ConveyorType conveyorType;
    [JsonProperty] private ItemType storedItem;

    private int currentOutput = 0;
    private bool itemInsertedThisTick = false;

    private List<Vector2Int> inputPositions;
    private List<Vector2Int> outputPositions;

    private ConveyorVisual visual;

    public Conveyor(Vector2Int gridPosition, Direction direction, ConveyorType conveyorType) : base(gridPosition, direction)
    {
        this.conveyorType = conveyorType;
    }

    public override void OnClick() { }
    public override bool CanInsert() => storedItem == null;
    public override bool CanInsertByPlayer() => false;
    public override bool CanExtract() => false;
    public override ItemStack Extract() => throw new System.InvalidOperationException();

    public override void Insert(ItemStack itemStack)
    {
        SetStoredItem(itemStack.GetItemType());
        itemInsertedThisTick = true;
    }

    public override void Destroy()
    {
        if (storedItem) PlayerInventory.instance.StoreSingle(storedItem);
        DisconnectEvents();
        base.Destroy();
    }

    public override BuildingType GetBuildingType()
    {
        return conveyorType;
    }

    protected override void InitializeData()
    {
        base.InitializeData();
        inputPositions = conveyorType.GetInputPositions().ConvertAll(RelativeToAbsolute);
        outputPositions = conveyorType.GetOutputPositions().ConvertAll(RelativeToAbsolute);
        ConnectEvents();
    }

    protected override void CreateVisuals()
    {
        visual = ConveyorVisual.Create();
        visual.Initialize(this);
        visual.RenderItem(storedItem);
    }

    protected override BuildingVisual GetVisuals()
    {
        return visual;
    }

    private void SetStoredItem(ItemType item)
    {
        storedItem = item;
        visual.RenderItem(item);
    }

    private void OnPretick()
    {
        itemInsertedThisTick = false;
    }

    private void MoveItem()
    {
        if (storedItem != null && !itemInsertedThisTick) DoInsertCycle();
        if (storedItem == null) DoExtractCycle();
    }

    private void DoInsertCycle()
    {
        Building target = GetInsertionTarget();
        if (target != null) InsertTo(target);
    }

    private void DoExtractCycle()
    {
        Building target = GetExtractionTarget();
        if (target != null) ExtractFrom(target);
    }

    private Building GetInsertionTarget()
    {
        for (int i = 0; i < outputPositions.Count; i++)
        {
            Building building = GetNextOutput();
            if (building != null && building.CanInsert()) return building;
        }
        return null;
    }

    private Building GetNextOutput()
    {
        currentOutput = (currentOutput + 1) % outputPositions.Count;
        return GridSystem.instance.GetBuildingAt(outputPositions[currentOutput]);
    }

    private Building GetExtractionTarget()
    {
        foreach (Vector2Int position in inputPositions)
        {
            Building building = GridSystem.instance.GetBuildingAt(position);
            if (building != null && building.CanExtract()) return building;
        }
        return null;
    }

    private void InsertTo(Building target)
    {
        target.Insert(new ItemStack(storedItem, 1));
        SetStoredItem(null);
    }

    private void ExtractFrom(Building target)
    {
        SetStoredItem(target.Extract().GetItemType());
    }

    private void ConnectEvents()
    {
        TickHandler.instance.TickConveyors += MoveItem;
        TickHandler.instance.PretickConveyors += OnPretick;
    }

    private void DisconnectEvents()
    {
        TickHandler.instance.TickConveyors -= MoveItem;
        TickHandler.instance.TickConveyors -= OnPretick;
    }
}
