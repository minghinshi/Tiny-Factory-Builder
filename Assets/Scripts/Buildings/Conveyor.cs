using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Conveyor : Building
{
    [SerializeField] private ItemType storedItem;

    private int currentOutput = 0;
    private bool itemInsertedThisTick = false;

    private SpriteRenderer itemRenderer;
    private List<Vector2Int> inputPositions;
    private List<Vector2Int> outputPositions;

    public Conveyor(Vector2Int gridPosition, Direction direction, ConveyorType conveyorType) : base(gridPosition, direction, conveyorType)
    {
        SetInputCells(conveyorType.GetInputPositions());
        SetOutputCells(conveyorType.GetOutputPositions());
        CreateItemRenderer();
        ConnectEvents();
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
        DestroyConveyor();
        base.Destroy();
    }

    private void SetInputCells(List<Vector2Int> relativePositions)
    {
        inputPositions = relativePositions.ConvertAll(RelativeToAbsolute);
    }

    private void SetOutputCells(List<Vector2Int> relativePositions)
    {
        outputPositions = relativePositions.ConvertAll(RelativeToAbsolute);
    }

    private void CreateItemRenderer()
    {
        Transform rendererTransform = new GameObject("ItemDisplay", typeof(SpriteRenderer)).transform;
        rendererTransform.SetParent(transform);
        rendererTransform.position = transform.position;
        itemRenderer = rendererTransform.GetComponent<SpriteRenderer>();
        itemRenderer.sortingOrder = 1;
    }

    private void SetStoredItem(ItemType item)
    {
        storedItem = item;
        itemRenderer.sprite = item.GetSprite();
    }

    private void RemoveStoredItem()
    {
        storedItem = null;
        itemRenderer.sprite = null;
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
            Building building = GridSystem.instance.GetBuildingAt(outputPositions[currentOutput]);
            currentOutput = (currentOutput + 1) % outputPositions.Count;
            if (building != null && building.CanInsert()) return building;
        }
        return null;
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
        RemoveStoredItem();
    }

    private void ExtractFrom(Building target)
    {
        SetStoredItem(target.Extract().GetItemType());
    }

    private void DestroyConveyor()
    {
        if (storedItem) Inventory.playerInventory.Store(storedItem, 1);
        DisconnectEvents();
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
