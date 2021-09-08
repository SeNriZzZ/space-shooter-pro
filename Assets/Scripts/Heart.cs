using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Player _player;

    [SerializeField] private AudioClip _powerupSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _powerupSound;
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.HeartPowerUp();
            }

            Destroy(this.gameObject);
            _audioSource.Play(0);
        }
    }
}