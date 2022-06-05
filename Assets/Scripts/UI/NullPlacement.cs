using UnityEngine;

public class NullPlacement : Placement
{
    public override void Update()
    {
        CheckInputs();
    }

    public override ItemType GetItemType()
    {
        return null;
    }

    protected override void CheckInputs() {
        if (Input.GetMouseButtonDown(0)) ClickOnMachine();
        base.CheckInputs();
    }

    private void ClickOnMachine()
    {
        Grids.buildingGrid.GetCellObjectAt(GetMouseGridPosition())?.OnClick();
    }
}
