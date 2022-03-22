using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
    private Direction direction;
    private BuildingType buildingType;

    public Building(Cell primaryCell, Direction direction, BuildingType buildingType)
    {
        this.direction = direction;
        this.buildingType = buildingType;
    }
}
