using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {north, east, south, west };

public static class DirectionHelper
{
    public static float GetRotationInDegrees(Direction direction) {
        return direction switch
        {
            Direction.north => 0f,
            Direction.east => -90f,
            Direction.south => 180f,
            Direction.west => 90f,
            _ => throw new System.Exception("Direction does not exist!")
        };
    }

    public static Direction RotateClockwise(Direction direction) {
        return direction switch
        {
            Direction.north => Direction.east,
            Direction.east => Direction.south,
            Direction.south => Direction.west,
            Direction.west => Direction.north,
            _ => throw new System.Exception("Direction does not exist!")
        };
    }

    public static Quaternion GetRotationQuaternion(Direction direction) {
        return Quaternion.Euler(0f, 0f, GetRotationInDegrees(direction));
    }

    public static Vector2Int TransformSize(Direction direction, Vector2Int size) {
        return direction switch
        {
            Direction.north => size,
            Direction.south => size,
            Direction.east => new Vector2Int(size.y, size.x),
            Direction.west => new Vector2Int(size.y, size.x),
            _ => throw new System.Exception("Direction does not exist!"),
        };
    }
}