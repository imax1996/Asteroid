using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ScreenBorder : MonoBehaviour
{
    private SphereCollider  _sphereCollider;
    private Vector2         _borderSizeWithRadius;

    private float offsetAfterChangePosition = -0.95f;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _borderSizeWithRadius = CameraBorder.GetCoordBorderCamera(Camera.main) + Vector2.one * _sphereCollider.radius * transform.localScale;
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;

        if (Mathf.Abs(transform.position.x) > _borderSizeWithRadius.x)
        {
            newPosition = new Vector2(newPosition.x * offsetAfterChangePosition, newPosition.y);
        }

        if (Mathf.Abs(transform.position.y) > _borderSizeWithRadius.y)
        {
            newPosition = new Vector2(newPosition.x, newPosition.y * offsetAfterChangePosition);
        }

        transform.position = newPosition;
    }
}
