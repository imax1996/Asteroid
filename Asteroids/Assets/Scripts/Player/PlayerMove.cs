using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _speedOfRotate = 3f;

    private AudioSource _audioSource;
    private Rigidbody   _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerEvents.MoveEvent += OnMove;
        PlayerEvents.RotateEvent += OnRotate;
        PlayerEvents.RotateMouseEvent += OnMouseRotate;
    }

    private void OnDisable()
    {
        PlayerEvents.MoveEvent -= OnMove;
        PlayerEvents.RotateEvent -= OnRotate;
        PlayerEvents.RotateMouseEvent -= OnMouseRotate;
    }

    private void OnMove()
    {
        _rigidbody.AddForce(transform.right * _acceleration * Time.deltaTime, ForceMode.Acceleration);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    private void OnRotate(float angleDir)
    {
        Quaternion rotEuler = Quaternion.Euler(Vector3.forward * angleDir * _speedOfRotate * Time.deltaTime);
        _rigidbody.MoveRotation(_rigidbody.rotation * rotEuler);
    }

    private void OnMouseRotate(Vector3 mousePosition)
    {
        Vector3 look = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        Quaternion to = Quaternion.Euler(new Vector3(0, 0, rotZ));
        Quaternion rot = Quaternion.RotateTowards(_rigidbody.rotation, to, _speedOfRotate * Time.deltaTime);
        _rigidbody.MoveRotation(rot);
    }
}
