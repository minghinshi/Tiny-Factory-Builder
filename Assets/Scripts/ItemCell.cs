using UnityEngine;

public class ItemCell : Cell
{
    private bool blocked = true;

    public delegate void CellOccupiedHandler(ItemCell itemCell);
    public event CellOccupiedHandler CellOccupied;

    public ItemCell(Vector2Int gridPosition, GridSystem gridSystem) : base(gridPosition, gridSystem)
    {

    }

    public bool IsBlocked()
    {
        return blocked;
    }

    public override bool CanInsert()
    {
        return !(occupied || blocked);
    }

    public override void OccupyCell(CellObject cellObject)
    {
        base.OccupyCell(cellObject);
        CellOccupied?.Invoke(this);
    }

    public void BlockCell()
    {
        blocked = true;
    }

    public void UnblockCell()
    {
        blocked = false;
    }

    public void MoveCellObjectTo(Cell destination)
    {
        if (!destination.CanInsert()) return;
        if (!occupied) return;
        if (containedObject.IsMovedThisTick()) return;

        containedObject.MoveTo(destination.CentreWorldPosition);
        destination.TryOccupyCell(containedObject);
        EmptyCell();
    }
}
