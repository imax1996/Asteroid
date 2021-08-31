using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;

    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private float[] _scalesOfAsteroids;

    private List<GameObject> _allAsteroids;
    private Stack<GameObject> _asteroidPool;
    private Vector2         _borderMainCamera;

    [SerializeField] private float _minSpeed = 1;
    [SerializeField] private float _maxSpeed = 3;
    [SerializeField] private float _rotateInSeparation = 45;

    private GameObject _anchorAsteroid;

    private AudioSource _audioSource;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _allAsteroids = new List<GameObject>();
        _asteroidPool = new Stack<GameObject>();
        _borderMainCamera = CameraBorder.GetCoordBorderCamera(Camera.main);
        _anchorAsteroid = new GameObject("AnchorAsteroid");
    }

    public void Restart()
    {
        if (_allAsteroids.Count > 0)
        {
            foreach (var asteroid in _allAsteroids)
            {
                if (asteroid.activeInHierarchy)
                {
                    DestroyAsteroid(asteroid, false);
                }
            }
        }
    }

    public void CreateNewAsteroid(int countOfNewAsteroids)
    {
        for (int i = 0; i < countOfNewAsteroids; i++)
        {
            Vector2 posToCreate = CameraBorder.GetRandomCoordOnBorder(_borderMainCamera);
            float scale = _scalesOfAsteroids[0];
            Vector2 velocity = Random.insideUnitCircle.normalized * Random.Range(_minSpeed, _maxSpeed);

            InstantiateAsteroid(posToCreate, Quaternion.identity, scale, velocity);
        }
    }

    private void InstantiateAsteroid(Vector3 position, Quaternion rot, float scale, Vector2 velocity)
    {
        GameObject asteroid = null;

        if (_asteroidPool.Count > 0)
        {
            asteroid = _asteroidPool.Pop();
            asteroid.SetActive(true);
        }
        else
        {
            asteroid = Instantiate(_asteroidPrefab, _anchorAsteroid.transform);
            asteroid.GetComponent<Asteroid>()._asteroidController = this;
            _allAsteroids.Add(asteroid);
        }

        asteroid.transform.position = position;
        asteroid.transform.rotation = rot;
        asteroid.transform.localScale = Vector3.one * scale;

        Rigidbody rigidbody = asteroid.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(velocity, ForceMode.VelocityChange);

        rigidbody.angularVelocity = new Vector3(Random.value, Random.value, Random.value);

        _levelController.CountOfAsteroids++;
    }

    public void DestroyAsteroid(GameObject asteroid, bool needSeparate)
    {
        if (needSeparate)
        {
            TryToSeparate(asteroid);
        }
        asteroid.SetActive(false);
        _asteroidPool.Push(asteroid);
        _levelController.CountOfAsteroids--;
        _audioSource.Play();
    }

    private void TryToSeparate(GameObject asteroid)
    {
        float oldScale = asteroid.transform.localScale.x;

        if (!Mathf.Approximately(oldScale, _scalesOfAsteroids[_scalesOfAsteroids.Length - 1]))
        {
            Vector2 pos = asteroid.transform.position;
            float scale = DecreaseScale(oldScale);
            Vector2 velocity = asteroid.GetComponent<Rigidbody>().velocity.normalized;

            InstantiateAsteroid(pos, Quaternion.identity, scale, Quaternion.Euler(0, 0, _rotateInSeparation) * velocity);
            InstantiateAsteroid(pos, Quaternion.identity, scale, Quaternion.Euler(0, 0, -_rotateInSeparation) * velocity);
        }
    }

    private float DecreaseScale(float oldScale)
    {
        int listCount = 0;
        foreach (var scaleInList in _scalesOfAsteroids)
        {
            listCount++;
            if (Mathf.Approximately(oldScale, scaleInList))
            {
                break;
            }
        }
        return _scalesOfAsteroids[listCount];
    }
}
