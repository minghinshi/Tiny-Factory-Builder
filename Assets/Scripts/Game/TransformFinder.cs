using UnityEngine;

public static class TransformFinder
{
    public static readonly Transform worldTransform = GameObject.Find("WorldCanvas").transform;
}
