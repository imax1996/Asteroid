using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IDamagable
{
    public GameObject owner;
    public Stack<GameObject> _offBullets;
    public float _timeToOff = 0;

    private void Update()
    {
        _timeToOff -= Time.deltaTime;
        if (_timeToOff <= 0)
        {
            OffBullet();
        }
    }

    public void OffBullet()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
        _offBullets.Push(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(typeof(IDamagable), out Component component);

        if (component is Asteroid)
        {
            switch (other.transform.localScale.x)
            {
                case 3:
                    TryToAddScore(20);
                    break;
                case 2:
                    TryToAddScore(50);
                    break;
                case 1:
                    TryToAddScore(100);
                    break;
            }
            OffBullet();
        }
        else if (component is UFO)
        {
            if (owner.TryGetComponent(out Player player)) {
                TryToAddScore(200);
                OffBullet();
            }
        }
        else if (component is Player)
        {
            if (owner.TryGetComponent(out UFO ufo))
            {
                OffBullet();
            }
        }
    }

    private void TryToOff()
    {
        if (owner.TryGetComponent(out Player player))
        {
            OffBullet();
        }

    }

    private void TryToAddScore(int score)
    {
        if (owner.TryGetComponent(out Player player))
        {
            player.Score += score;
        }
    }
}
