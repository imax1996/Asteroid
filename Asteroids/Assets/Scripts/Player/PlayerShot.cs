using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private BulletController   _bulletController;
    [SerializeField] private Material           _bulletMaterial;
    [SerializeField] private GameObject         _gun;
    [SerializeField] private AudioSource        _audioSource;
    [SerializeField] private int                _fireRate = 3;

    private float _timeToShot = 0;

    private void OnEnable()
    {
        PlayerEvents.ShotEvent += OnShot;
    }

    private void OnDisable()
    {
        PlayerEvents.ShotEvent -= OnShot;
    }

    private void OnShot()
    {
        if (Time.timeSinceLevelLoad < _timeToShot + 1f/ _fireRate || Time.timeScale == 0)
        {
            return;
        }
        Vector3 gunPos = _gun.transform.position;
        Vector3 directionToShot = (gunPos - transform.position).normalized;
        _bulletController.OnShot(_bulletMaterial, gunPos, directionToShot, gameObject);
        _timeToShot = Time.timeSinceLevelLoad;
        _audioSource.Play();
    }
}
