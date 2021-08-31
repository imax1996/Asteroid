using UnityEngine;

public class UFOShot : MonoBehaviour
{
    [SerializeField] private BulletController   _bulletController;
    [SerializeField] private Material           _bulletMaterial;
    [SerializeField] private GameObject         _playerShip;
    [SerializeField] private AudioSource        _audioSource;

    [SerializeField] private Vector2            _fireRate = new Vector2(2f, 5f);

    private float _timeToShot = 1;

    private void Update()
    {
        _timeToShot -= Time.deltaTime;

        if (_timeToShot <= 0 && Time.timeScale != 0)
        {
            Shot();
        }
    }

    private void Shot()
    {
        Vector3 directionToShot = (_playerShip.transform.position - transform.position).normalized;
        _bulletController.OnShot(_bulletMaterial, transform.position, directionToShot, gameObject);
        _timeToShot = Random.Range(_fireRate.x, _fireRate.y);
        _audioSource.Play();
    }
}
