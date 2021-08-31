using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamagable
{
    public event UnityAction        GameOverEvent;
    public event UnityAction<int>   HealthUIEvent;
    public event UnityAction<int>   ScoreUIEvent;

    [SerializeField] private PlayerAudioController  _audioController;
    [SerializeField] private GameObject             _model;
    [SerializeField] private int                    _startHealth = 3;
    [SerializeField] private float                  _timeOfInvulnerability = 3;
    [SerializeField] private float                  _blinkRate = 2;

    private Rigidbody   _rigidbody;
    private int         _health;
    private int         _score;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Max(0, value);
            HealthUIEvent?.Invoke(_health);

            if (value <= 0)
            {
                gameObject.SetActive(false);
                GameOverEvent?.Invoke();
            }
        }
    }
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            ScoreUIEvent?.Invoke(_score);
        }
    }

    private bool _isInvulnerable = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        Score = 0;
        Restart();
        Health = _startHealth;
    }

    private void Restart()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        _rigidbody.position = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
        StartCoroutine(AnimationInvulnerability());
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(typeof(IDamagable), out Component component);

        if (_isInvulnerable)
        {
            return;
        }

        if (component is Bullet && other.GetComponent<Bullet>()._owner == gameObject)
        {
            return;
        }

        Restart();
        Health--;
        _audioController._audioSource.Play();
    }

    private IEnumerator AnimationInvulnerability()
    {
        float timeToVulnerable = Time.timeSinceLevelLoad + _timeOfInvulnerability;
        _isInvulnerable = true;
        while (Time.timeSinceLevelLoad < timeToVulnerable)
        {
            _model.SetActive(!_model.activeInHierarchy);
            yield return new WaitForSeconds(0.5f/_blinkRate);
        }
        _isInvulnerable = false;
        _model.SetActive(true);
    }
}
