using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject _asteroid;
    [SerializeField] private GameObject[] _powerup;
    [SerializeField] private GameObject _asteroidUndestroyable;
    [SerializeField] private GameObject _heartPrefab;

    private bool _isDead = false;
    [SerializeField] private float _respawnRatio = 5f;
    [SerializeField] private float _gameMode;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnAsteroidRoutine());
        StartCoroutine(SpawnAsteroid());
        StartCoroutine(SpawnHeart());
    }

    void Update()
    {
        if (_isDead == true)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnAsteroidRoutine()
    {
        while (_isDead == false)
        {
            yield return new WaitForSeconds(Random.Range(40, 61));
            Instantiate(_asteroid, new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_isDead == false)
        {
            yield return new WaitForSeconds(_respawnRatio);
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-9f, 9f), 6, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (_isDead == false)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerup[randomPowerUp], new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while (_isDead == false)
        {
            yield return new WaitForSeconds(_gameMode);
            Instantiate(_asteroidUndestroyable, new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
        }
    }

    IEnumerator SpawnHeart()
    {
        while (_isDead == false)
        {
            yield return new WaitForSeconds(Random.Range(50f, 65f));
            Instantiate(_heartPrefab, new Vector3(Random.Range(-8f, 8f), 6, 0), Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _isDead = true;
    }
}