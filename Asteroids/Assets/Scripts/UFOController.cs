using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    [SerializeField] private UFOMove _ufoMove;
    private Vector2 _rangeTimeToSpawn = new Vector2(20f, 40f);
    [SerializeField] private float _timeToSpawn;
    private bool inGame = false;

    public void Init()
    {
        _ufoMove.OffUFO();
        _timeToSpawn = Random.Range(_rangeTimeToSpawn.x, _rangeTimeToSpawn.y);
        inGame = true;
    }

    private void Update()
    {
        if (!inGame || _ufoMove.isActiveAndEnabled)
        {
            return;
        }

        _timeToSpawn -= Time.deltaTime;

        if (_timeToSpawn <= 0)
        {
            _ufoMove.Spawn();
            _timeToSpawn = Random.Range(_rangeTimeToSpawn.x, _rangeTimeToSpawn.y);
        }
    }
}