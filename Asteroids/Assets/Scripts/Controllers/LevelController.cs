using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AsteroidController _asteroidController;
    [SerializeField] private BulletController   _bulletController;
    [SerializeField] private UFOController      _uFOController;

    [SerializeField] private float   _delayNextLevel = 2;

    private int     _level = 0;
    private int     _countOfAsteroids = 0;

    public int CountOfAsteroids
    {
        get { return _countOfAsteroids; }
        set
        {
            _countOfAsteroids = value;
            if (value <= 0)
            {
                StartCoroutine(StartNextLevel());
            }
        }
    }

    public void StartNewGame()
    {
        _level = 0;
        _asteroidController.Restart();
        _bulletController.Restart();
        _uFOController.Init();
        NextLevel();
    }

    private void NextLevel()
    {
        _level++;
        _asteroidController.CreateNewAsteroid(_level + 1);
    }

    private IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(_delayNextLevel);
        if (_countOfAsteroids == 0)
        {
            NextLevel();
        }
    }
}