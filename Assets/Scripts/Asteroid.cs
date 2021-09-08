using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _asteroidLife = 2;
    [SerializeField] private GameObject _enemy;

    private Player _player;

    private Animator _animation;

    private CircleCollider2D _collider;

    [SerializeField] private AudioClip _explosion;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosion;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<CircleCollider2D>();
        _animation = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.Damage();
            _player.Damage();
            _animation.SetTrigger("AsteroidDeath");
            _speed = 0.25f;
            Destroy(this.gameObject, 2f);
            _audioSource.Play(0);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            _asteroidLife--;
            Destroy(other.gameObject);
            if (_asteroidLife < 1)
            {
                _player.ScoreUp();
                _player.ScoreUp();
                _player.ScoreUp();
                _player.ScoreUp();
                _player.ScoreUp();
                _animation.SetTrigger("AsteroidDeath");
                _speed = 0.25f;
                _collider.enabled = false;
                Destroy(this.gameObject, 2f);
                _audioSource.Play(0);
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(_enemy, new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
                }
            }
        }
    }
}