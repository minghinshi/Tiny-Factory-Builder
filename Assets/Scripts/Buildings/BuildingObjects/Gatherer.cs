using Newtonsoft.Json;
using System;
using UnityEngine;

public class Gatherer : Producer
{
    [JsonProperty] private GathererType gathererType;

    public Gatherer(Vector2Int gridPosition, Direction direction, GathererType gathererType) : base(gridPosition, direction)
    {
        this.gathererType = gathererType;
    }

    public override bool CanInsert() => false;

    public override void Insert(ItemStack itemStack)
    {
        throw new InvalidOperationException();
    }

    public override BuildingType GetBuildingType()
    {
        return gathererType;
    }

    protected override Timer GetNewTimer()
    {
        return new Timer(150, true);
    }

    protected override void ProduceOutputs()
    {
        outputInventory.StoreSingle(gathererType.GetProducedItem());
    }
}
