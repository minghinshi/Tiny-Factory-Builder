using UnityEngine;

public abstract class BuildingVisual : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected abstract BuildingType GetBuildingType();
    protected abstract Vector2Int GetGridPosition();
    protected abstract Direction GetDirection();

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
        transform.localScale = (Vector3)(Vector3Int)GetBuildingType().GetSize();
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