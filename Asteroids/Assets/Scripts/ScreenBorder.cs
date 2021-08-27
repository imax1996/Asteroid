using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ScreenBorder : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    [SerializeField] private Vector2 _borderSizeWithRadius;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        Camera mainCamera = Camera.main;
        float borderX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect + _sphereCollider.radius;
        float borderY = mainCamera.transform.position.y + mainCamera.orthographicSize + _sphereCollider.radius;
        _borderSizeWithRadius = new Vector2(borderX, borderY);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > _borderSizeWithRadius.x)
        {
            transform.position = new Vector2(transform.position.x * -0.95f, transform.position.y);
        }


        if (Mathf.Abs(transform.position.y) > _borderSizeWithRadius.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y * -0.95f);
        }
    }
}
