using UnityEngine;

public class UFO : MonoBehaviour, IDamagable
{
    [SerializeField] private UFOController _ufocontroller;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(typeof(IDamagable), out Component component);

        if (component is Bullet && other.GetComponent<Bullet>()._owner == gameObject)
        {
            return;
        }

        gameObject.SetActive(false);
        _ufocontroller.OnDeath();
    }
}
