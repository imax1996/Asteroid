using UnityEngine;

public class UFO : MonoBehaviour, IDamagable
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(typeof(IDamagable), out Component component);

        if (component is Bullet && other.GetComponent<Bullet>().owner == gameObject)
        {
            return;
        }

        gameObject.SetActive(false);
    }
}
