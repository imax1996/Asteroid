using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _speedOfRotate = 3f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerEvents.MoveEvent += Move;
        PlayerEvents.RotateEvent += Rotate;
        PlayerEvents.RotateMouseEvent += MouseRotate;
    }

    private void OnDisable()
    {
        PlayerEvents.MoveEvent -= Move;
        PlayerEvents.RotateEvent -= Rotate;
        PlayerEvents.RotateMouseEvent -= MouseRotate;
    }

    private void Move()
    {
        _rigidbody.AddForce(transform.right * _acceleration * Time.deltaTime, ForceMode.Acceleration);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }

    private void Rotate(float angleDir)
    {
        Quaternion rotEuler = Quaternion.Euler(Vector3.forward * angleDir * _speedOfRotate * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * rotEuler);
    }

    private void MouseRotate(Vector3 mousePosition)
    {
        Vector3 look = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        Quaternion to = Quaternion.Euler(new Vector3(0, 0, rotZ));
        Quaternion rot = Quaternion.RotateTowards(_rigidbody.rotation, to, _speedOfRotate * Time.deltaTime);
        _rigidbody.MoveRotation(rot);
    }
}
