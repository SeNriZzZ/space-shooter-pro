using UnityEngine;

public class AsteroidUndestroyable : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _rotationSpeed = 3f;

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
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.Damage();
            _animation.SetTrigger("AsteroidDeath");
            _speed = 0.5f;
            _collider.enabled = false;
            Destroy(this.gameObject, 2f);
            _audioSource.Play(0);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
        }
    }
}