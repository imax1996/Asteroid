using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
