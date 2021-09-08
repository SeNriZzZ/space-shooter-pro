using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int powerUpID; //0 - tripleshot; 1 - speedup; 2 - shield;

    [SerializeField] private AudioClip _pickUpSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
        _audioSource.clip = _pickUpSound;
    }

    // Update is called once per frame
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
        if (other.tag == "Player")
        {
            Player
                player = other.transform
                    .GetComponent<Player>(); // Accessing The Player script to get any of the compontnts there.
            switch (powerUpID)
            {
                case 0:
                    player.TripleShotActive();
                    Destroy(this.gameObject);
                    _audioSource.Play(0);
                    break;
                case 1:
                    player.SpeedUpActive();
                    Destroy(this.gameObject);
                    _audioSource.Play(0);
                    break;
                case 2:
                    player.ShieldUp();
                    Destroy(this.gameObject);
                    _audioSource.Play(0);
                    break;
            }
        }
    }
}