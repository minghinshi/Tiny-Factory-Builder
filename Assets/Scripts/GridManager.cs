using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager
{
    public static GridManager instance;
    public static GridSystem buildingGrid = new GridSystem(1000, 1000, 1f);
}
