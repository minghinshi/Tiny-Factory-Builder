using UnityEngine;

public abstract class BuildingVisual : MonoBehaviour
{
    public static readonly Transform worldTransform = GameObject.Find("WorldCanvas").transform;

    protected SpriteRenderer spriteRenderer;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected abstract BuildingType GetBuildingType();
    protected abstract Vector2Int GetGridPosition();
    protected abstract Direction GetDirection();

    protected static GameObject CreateGameObject()
    {
        GameObject gameObject = new();
        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<SpriteRenderer>();
        return gameObject;
    }

    protected void Initialize()
    {
        InitializeSpriteRenderer();
        InitializeTransform();
    }

    private void InitializeSpriteRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetBuildingType().GetSprite();
    }

    private void InitializeTransform()
    {
        transform.SetPositionAndRotation(GetPosition(), GetRotation());
        transform.SetParent(worldTransform);
        transform.localScale = (Vector3)(Vector3Int)GetBuildingType().GetSize();
        GetComponent<RectTransform>().sizeDelta = Vector2.one;
    }

    private Vector3 GetPosition()
    {
        return GridSystem.instance.GetWorldPosition(GetGridPosition(), GetBuildingType().GetTransformedSize(GetDirection()));
    }

    private Quaternion GetRotation()
    {
        return GetDirection().GetRotationQuaternion();
    }
}