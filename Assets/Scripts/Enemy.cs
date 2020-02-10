using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    private float _randomRotation = 0;
    [SerializeField]
    private int _lives = 1;
    public int enemyLevel = 1;

    private Player _player;

    [SerializeField]
    private GameObject _enemyDobleLaser;
    [SerializeField]
    private GameObject _enemyTripleLaser;
    [SerializeField]
    private GameObject _enemyDamage;

    [SerializeField]
    private GameObject _explosionPref;

    private void Start()
    {
        EnemyLevel();

        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            _enemyDamage.SetActive(false);
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    public void EnemyLevel()
    {
        switch (enemyLevel)
        {
            case 1:
                _speed = 2f;
                _lives = 1;
                StartCoroutine(EnemyShootRoutine1());
                break;
            case 2:
                _speed = 2.5f;
                _lives = 1;
                StartCoroutine(EnemyShootRoutine2());
                break;
            case 3:
                _speed = 3f;
                _lives = 1;
                StartCoroutine(EnemyShootRoutine2());
                break;
            case 4:
                _speed = 3.5f;
                _lives = 2;
                StartCoroutine(EnemyShootRoutine3());
                break;
            case 5:
                _speed = 4f;
                _lives = 2;
                StartCoroutine(EnemyShootRoutine3());
                break;
            case 6:
                _speed = 4.5f;
                _lives = 2;
                StartCoroutine(EnemyShootRoutine4());
                break;
            case 7:
                _speed = 5f;
                _lives = 2;
                StartCoroutine(EnemyShootRoutine5());
                break;

        }
    }

    IEnumerator EnemyShootRoutine1()
    {
        while (true)
        {
            float _randomTime = Random.Range(3f, 7f);
            int _randomSpawn = Random.Range(1, 2);

            switch (_randomSpawn)
            {
                case 1:
                    EnemyShoot();
                    break;
            }
            yield return new WaitForSeconds(_randomTime);
        }
    }

    IEnumerator EnemyShootRoutine2()
    {
        while (true)
        {
            float _randomTime = Random.Range(3f, 6f);
            int _randomSpawn = Random.Range(1, 3);

            switch (_randomSpawn)
            {
                case 1:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 2:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.5f);
                    EnemyShoot();
                    break;
            }
            yield return new WaitForSeconds(_randomTime);
        }
    }

    IEnumerator EnemyShootRoutine3()
    {
        while (true)
        {
            float _randomTime = Random.Range(2f, 5f);
            int _randomSpawn = Random.Range(1, 5);

            switch (_randomSpawn)
            {
                case 1:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.3f);
                    EnemyShoot();
                    break;
                case 2:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot();
                    break;
                case 3:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.3f);
                    EnemyShoot();
                    yield return new WaitForSeconds(0.3f);
                    EnemyShoot();
                    break;
                case 4:
                    EnemyShoot2();
                    break;
            }
            yield return new WaitForSeconds(_randomTime);
        }
    }

    IEnumerator EnemyShootRoutine4()
    {
        while (true)
        {
            float _randomTime = Random.Range(1f, 3f);
            int _randomSpawn = Random.Range(1, 4);

            switch (_randomSpawn)
            {
                case 1:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.3f);
                    EnemyShoot2();
                    break;
                case 2:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot();
                    break;
                case 3:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot2();
                    break;
            }
            yield return new WaitForSeconds(_randomTime);
        }
    }

    IEnumerator EnemyShootRoutine5()
    {
        while (true)
        {
            float _randomTime = Random.Range(1f, 3f);
            int _randomSpawn = Random.Range(1, 4);

            switch (_randomSpawn)
            {
                case 1:
                    EnemyShoot2();
                    yield return new WaitForSeconds(0.3f);
                    EnemyShoot2();
                    break;
                case 2:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot2();
                    break;
                case 3:
                    EnemyShoot();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot2();
                    yield return new WaitForSeconds(0.2f);
                    EnemyShoot2();
                    break;
            }
            yield return new WaitForSeconds(_randomTime);
        }
    }

    private void EnemyShoot()
    {
        Instantiate(_enemyDobleLaser, transform.position, Quaternion.identity);
    }

    private void EnemyShoot2()
    {
        Instantiate(_enemyTripleLaser, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            _enemyDamage.SetActive(false);

            if (player != null)
            {
                player.Damage();
            }
            GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            newExplosion.transform.localScale = transform.localScale;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Asteroid")
        {
            Moving_Asteroid asteroid = other.transform.GetComponent<Moving_Asteroid>();
            asteroid.lives--;

            _enemyDamage.SetActive(false);
            GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            newExplosion.transform.localScale = transform.localScale;
            Destroy(this.gameObject);
        }
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            if (_lives > 1)
            {
                _lives--;
                _player.AddToScore(10);
                _enemyDamage.SetActive(true);
            }
            else if (_lives <= 1)
            {
                _enemyDamage.SetActive(false);
                if (_player != null)
                {
                    _player.AddToScore(20);

                }
                _lives--;
                GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
                newExplosion.transform.localScale = transform.localScale;
                Destroy(this.gameObject);
            }

        }
        else if (other.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            if (_lives > 1)
            {
                _lives--;
                _enemyDamage.SetActive(true);
            }
            else if (_lives <= 1)
            {
                _enemyDamage.SetActive(false);
                _lives--;
                GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
                newExplosion.transform.localScale = transform.localScale;
                Destroy(this.gameObject);
            }
        }
    }
}
