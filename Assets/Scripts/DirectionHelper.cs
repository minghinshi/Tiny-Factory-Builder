using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {North, East, South, West };

public static class DirectionHelper
{
    public static float GetRotationInDegrees(this Direction direction) {
        return direction switch
        {
            Direction.North => 0f,
            Direction.East => -90f,
            Direction.South => 180f,
            Direction.West => 90f,
            _ => throw new System.Exception("Direction does not exist!")
        };
    }

    public static Direction RotateClockwise(this Direction direction) {
        return direction switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => throw new System.Exception("Direction does not exist!")
        };
    }

    public static Quaternion GetRotationQuaternion(this Direction direction) {
        return Quaternion.Euler(0f, 0f, direction.GetRotationInDegrees());
    }

    public static Vector2Int TransformSize(this Direction direction, Vector2Int size) {
        return direction switch
        {
            Direction.North => size,
            Direction.South => size,
            Direction.East => new Vector2Int(size.y, size.x),
            Direction.West => new Vector2Int(size.y, size.x),
            _ => throw new System.Exception("Direction does not exist!"),
        };
    }

    public static Vector2Int RotateVector(this Direction direction, Vector2Int vector)
    {
        return direction switch
        {
            Direction.North => vector,
            Direction.East => new Vector2Int(vector.y, -vector.x),
            Direction.South => -vector,
            Direction.West => new Vector2Int(-vector.y, vector.x),
            _ => throw new System.Exception("Direction does not exist!"),
        };
    }
}