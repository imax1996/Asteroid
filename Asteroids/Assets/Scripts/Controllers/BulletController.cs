using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    public event UnityAction<Material, Vector3, Vector3, GameObject> ShotEvent;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Material _materialShip;
    [SerializeField] private Material _materialUFO;

    private List<GameObject> _allBullets;
    private Stack<GameObject> _bulletPool;
    private float _widthScreen;
    private GameObject _achorBullet;

    private void Awake()
    {
        _allBullets = new List<GameObject>();
        _widthScreen = Camera.main.orthographicSize * 2;
        _bulletPool = new Stack<GameObject>();
        ShotEvent += CreateBullet;
        _achorBullet = new GameObject("AnchorBullet");
    }

    public void Restart()
    {
        if (_allBullets.Count > 0)
        {
            foreach (var bullet in _allBullets)
            {
                if (bullet.activeInHierarchy == true)
                {
                    bullet.GetComponent<Bullet>().OffBullet();
                }
            }
        }
    }

    public void OnShot(Material material, Vector3 posToCreateBullet, Vector3 velocity, GameObject owner)
    {
        ShotEvent?.Invoke(material, posToCreateBullet, velocity, owner);
    }

    private void CreateBullet(Material material, Vector3 posToCreateBullet, Vector3 direction, GameObject owner)
    {
        GameObject bullet;

        if (_bulletPool.Count > 0)
        {
            bullet = _bulletPool.Pop();
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(_bulletPrefab, _achorBullet.transform);
            _allBullets.Add(bullet);
        }

        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * _speed, ForceMode.VelocityChange);
        rigidbody.MoveRotation(Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * direction));

        bullet.transform.position = posToCreateBullet;

        Bullet bulletClass = bullet.GetComponent<Bullet>();
        bulletClass._offBullets = _bulletPool;
        bulletClass._timeToOff = _widthScreen / _speed;
        bulletClass.owner = owner;

        bullet.GetComponent<Renderer>().material = material;
    }
}
