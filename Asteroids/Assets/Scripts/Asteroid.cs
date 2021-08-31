using UnityEngine;

public class Asteroid : MonoBehaviour, IDamagable
{
    public AsteroidController _asteroidController;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(typeof(IDamagable), out Component component);

        if (component is Player)
        {
            _asteroidController.DestroyAsteroid(gameObject, false);
        }
        else if (component is Bullet)
        {
            _asteroidController.DestroyAsteroid(gameObject, true);
        }
        else if (component is UFO)
        {
            _asteroidController.DestroyAsteroid(gameObject, false);
        }
    }
}
