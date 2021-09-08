using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _playerLife = 3;
    [SerializeField] private float _playerSpeed = 3f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _superFireRate = 30f;
    private float _canFire = 0f;
    private float _canFireSuper = 0f;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _tripleShotPrefabMiddle;
    [SerializeField] private GameObject _tripleShotPrefabRight;
    [SerializeField] private GameObject _tripleShotPrefabLeft;
    [SerializeField] private GameObject _superLaser;

    [SerializeField] private bool _tripleShotActive = false;
    [SerializeField] private bool _speedUpActive = false;
    [SerializeField] private bool _shieldActive = false;

    [SerializeField] private GameObject _shieldEffect;

    [SerializeField] private int _score;

    private UIManager _uiManager;

    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;

    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private AudioClip _superLaserSound;
    private AudioSource _audioSource;

    private float CoolDown = 30f;
    private float CoolDownTimer = 0f;
    public string CoolDownReady = "READY";


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    void Update()
    {
        if (CoolDownTimer < 0)
        {
            CoolDownTimer = 0;
        }

        if (CoolDownTimer > 0)
        {
            CoolDownTimer -= Time.deltaTime;
        }

        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            fireLaser();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
        {
            fireLaser();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > _canFireSuper)
        {
            StartCoroutine(fireSuperLaser());
            CoolDownTimer = CoolDown;
        }

        if (CoolDownTimer == 0)
        {
            _uiManager.Cooldown(CoolDownReady);
        }
    }

    IEnumerator ShowCooldown()
    {
        while (Time.time < _canFireSuper)
        {
            string CoolDownShow = string.Format("{0:0,0}", CoolDownTimer);
            _uiManager.Cooldown(CoolDownShow);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator fireSuperLaser()
    {
        _audioSource.clip = _superLaserSound;
        if (_uiManager.GameIsPaused == false)
        {
            _canFireSuper = Time.time + _superFireRate;
            _superLaser.SetActive(true);
            _audioSource.Play(0);
            yield return new WaitForSeconds(5f);
            _superLaser.SetActive(false);
        }

        if (Time.time < _canFireSuper)
        {
            Debug.Log("CoolDown");
            StartCoroutine(ShowCooldown());
        }
    }

    void fireLaser()
    {
        _audioSource.clip = _laserSound;
        if (_uiManager.GameIsPaused == false)
        {
            _canFire = Time.time + _fireRate;
            if (_tripleShotActive == true)
            {
                Instantiate(_tripleShotPrefabMiddle,
                    new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z),
                    Quaternion.identity);
                Instantiate(_tripleShotPrefabRight,
                    new Vector3(transform.position.x + 0.78f, transform.position.y - 0.3f, transform.position.z),
                    Quaternion.identity);
                Instantiate(_tripleShotPrefabLeft,
                    new Vector3(transform.position.x - 0.78f, transform.position.y - 0.3f, transform.position.z),
                    Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab,
                    new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z),
                    Quaternion.identity);
            }

            _audioSource.Play(0);
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * _playerSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _playerSpeed * Time.deltaTime);


        //Also can use CLAMPING
        if (transform.position.y >= 1)
        {
            transform.position = new Vector3(transform.position.x, 1, 0);
        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }


        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(-10, transform.position.y, 0);
        }
        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(10, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shieldEffect.SetActive(false);
            return;
        }

        _playerLife -= 1;

        _uiManager.UpdateLives(_playerLife);

        if (_playerLife < 1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOver();
            Destroy(this.gameObject);
        }

        if (_playerLife == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if (_playerLife == 1)
        {
            _leftEngine.SetActive(true);
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(5f);
        _tripleShotActive = false;
    }

    public void SpeedUpActive()
    {
        _speedUpActive = true;
        StartCoroutine(DeactivateSpeedUp());
        _playerSpeed = 9f;
    }

    IEnumerator DeactivateSpeedUp()
    {
        yield return new WaitForSeconds(10f);
        _speedUpActive = false;
        _playerSpeed = 6f;
    }

    public void ShieldUp()
    {
        _shieldActive = true;
        _shieldEffect.SetActive(true);
    }

    public void ScoreUp()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

    public void ScoreDown()
    {
        _score -= 5;
        _uiManager.UpdateScore(_score);
    }

    public void HeartPowerUp()
    {
        if (_playerLife < 3)
        {
            _playerLife += 1;
            _uiManager.UpdateLives(_playerLife);
        }

        if (_playerLife == 3)
        {
            _rightEngine.SetActive(false);
        }
        else if (_playerLife == 2)
        {
            _leftEngine.SetActive(false);
        }
    }
}