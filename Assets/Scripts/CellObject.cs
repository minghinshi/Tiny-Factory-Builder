using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellObject
{
    protected Cell primaryCell;

    public Vector2Int GetGridPosition() {
        return primaryCell.GetGridPosition();
    }

    public abstract Vector2Int GetSize();
    public abstract void Destroy();
}
