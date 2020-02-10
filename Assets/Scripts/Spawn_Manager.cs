using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerupsPrefab;
    [SerializeField]
    private GameObject _asteroidPrefab;
    public int spawnManagerLevel = 1;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnAsteroidsRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false && spawnManagerLevel == 1)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.8f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 2)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.6f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 3)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.2f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 4)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 5)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 6)
        {
            float randomX = Random.Range(-9, 9);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.8f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        Debug.Log("routine Started");
        yield return new WaitForSeconds(6.0f);
        while (_stopSpawning == false && spawnManagerLevel == 1)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
        while (_stopSpawning == false && spawnManagerLevel == 2)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(6);
        }
        while (_stopSpawning == false && spawnManagerLevel == 3)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(6);
        }
        while (_stopSpawning == false && spawnManagerLevel == 4)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
        while (_stopSpawning == false && spawnManagerLevel == 5)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
        while (_stopSpawning == false && spawnManagerLevel == 6)
        {
            float randomX = Random.Range(-9, 9);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupsPrefab[randomPowerup], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator SpawnAsteroidsRoutine()
    {
        while (_stopSpawning == false && spawnManagerLevel == 1)
        {

            yield return new WaitForSeconds(3f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 2)
        {

            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(6.0f);
        while (_stopSpawning == false && spawnManagerLevel == 3)
        {
            float _randomSizeX = Random.Range(0.5f, 1.5f);
            float _randomSizeY = Random.Range(0.5f, 1.5f);
            float randomX = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newAsteroid.transform.localScale = new Vector3(_randomSizeX, _randomSizeY, 0);
            yield return new WaitForSeconds(4f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 4)
        {
            float _randomSizeX = Random.Range(0.5f, 1.2f);
            float _randomSizeY = Random.Range(0.5f, 1.2f);
            float randomX = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newAsteroid.transform.localScale = new Vector3(_randomSizeX, _randomSizeY, 0);
            yield return new WaitForSeconds(3f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 5)
        {
            float _randomSizeX = Random.Range(0.1f, 0.7f);
            float _randomSizeY = Random.Range(0.1f, 0.7f);
            float randomX = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newAsteroid.transform.localScale = new Vector3(_randomSizeX, _randomSizeY, 0);
            yield return new WaitForSeconds(1.8f);
        }
        while (_stopSpawning == false && spawnManagerLevel == 6)
        {
            float _randomSizeX = Random.Range(0.08f, 0.3f);
            float _randomSizeY = Random.Range(0.08f, 0.3f);
            float randomX = Random.Range(-9, 9);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            newAsteroid.transform.localScale = new Vector3(_randomSizeX, _randomSizeY, 0);
            yield return new WaitForSeconds(0.7f);
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
