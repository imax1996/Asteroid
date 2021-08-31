using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [HideInInspector] public AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
