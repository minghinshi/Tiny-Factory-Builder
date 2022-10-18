using UnityEngine;

public static class Transforms
{
    public static readonly Transform worldTransform = GameObject.Find("WorldCanvas").transform;
    public static readonly Transform UITransform = GameObject.Find("UICanvas").transform;
}
