using System.Collections.Generic;
using UnityEngine;

public class Conveyor : Building
{
    private ItemType storedItem;
    private SpriteRenderer itemRenderer;

    private List<Cell> inputCells;
    private List<Cell> outputCells;

    private int currentOutput = 0;
    private bool itemInsertedThisTick = false;

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
        inputCells = RelativePositionsToCells(relativePositions);
    }

    private void SetOutputCells(List<Vector2Int> relativePositions)
    {
        outputCells = RelativePositionsToCells(relativePositions);
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
        for (int i = 0; i < outputCells.Count; i++)
        {
            Building target = CycleTarget();
            if (target != null && target.CanInsert())
            {
                InsertTo(target);
                break;
            }
        }
    }

    private Building CycleTarget()
    {
        Building building = GetTarget();
        SetNextOutput();
        return building;
    }

    private Building GetTarget()
    {
        return outputCells[currentOutput].GetContainedBuilding();
    }

    private void SetNextOutput()
    {
        currentOutput = (currentOutput + 1) % outputCells.Count;
    }

    private void InsertTo(Building target)
    {
        target.Insert(new ItemStack(storedItem, 1));
        RemoveStoredItem();
    }

    private void DoExtractCycle()
    {
        inputCells.ForEach(CheckInputCell);
    }

    private void CheckInputCell(Cell inputCell)
    {
        Building target = inputCell.GetContainedBuilding();
        if (storedItem == null && target != null && target.CanExtract()) ExtractFrom(target);
    }

    private void ExtractFrom(Building target)
    {
        SetStoredItem(target.Extract().GetItemType());
    }

    private void DestroyConveyor()
    {
        if (storedItem) PlayerInventory.inventory.Store(storedItem, 1);
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
