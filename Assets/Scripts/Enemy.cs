using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Player _player;

    private Animator m_Animator;
    private BoxCollider2D _collider;

    [SerializeField] private AudioClip _explosion;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosion;
        _player = GameObject.Find("Player").GetComponent<Player>();
        m_Animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            float RandomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(RandomX, 6, 0);
            _player.ScoreDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.transform.GetComponent<Player>().Damage();
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            m_Animator.SetTrigger("EnemyDeath");
            _collider.enabled = false;
            _speed = 1f;
            Destroy(this.gameObject, 1.15f);
            _audioSource.Play(0);
        }

        if (other.gameObject.CompareTag("Laser"))
        {
            _player.ScoreUp();

            m_Animator.SetTrigger("EnemyDeath");
            _collider.enabled = false;
            _speed = 1f;

            Destroy(this.gameObject, 1.15f);
            Destroy(other.gameObject);
            _audioSource.Play(0);
        }

        if (other.gameObject.CompareTag("SuperLaser"))
        {
            _player.ScoreUp();

            m_Animator.SetTrigger("EnemyDeath");
            _collider.enabled = false;
            _speed = 1f;

            Destroy(this.gameObject, 1.15f);
            _audioSource.Play(0);
        }
    }
}