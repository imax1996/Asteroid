using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMove : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Camera _camera;
    private float _percentOutBorderScreen = 20;
    private float _timeToFlyOnWidth = 10;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Spawn()
    {
        Vector2 coordBorder = CameraBorder.GetCoordBorderCamera(_camera);

        float posX = coordBorder.x * Mathf.Sign(Random.Range(-1f, 1f));
        float posY = Random.Range(-coordBorder.y, coordBorder.y) * (100f- _percentOutBorderScreen) / 100f;

        transform.position = new Vector2(posX, posY);

        gameObject.SetActive(true);

        _rigidbody.velocity = Vector3.zero;
        float speed = coordBorder.x * 2 / _timeToFlyOnWidth;
        _rigidbody.AddForce(Vector3.right * Mathf.Sign(-posX) * speed, ForceMode.VelocityChange);
    }

    public void OffUFO()
    {
        gameObject.SetActive(false);
    }
}
