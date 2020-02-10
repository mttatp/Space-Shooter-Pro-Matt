using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _boostSpeed = 1.5f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canshoot = -1f;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _score;
    [SerializeField]
    private int _currentLevel = 1;

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShoot;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _lowDamage, _highDamage;
    [SerializeField]
    private GameObject _explosionPref;

    [SerializeField]
    private Animator _anim;


    private Vector3 _offSet = new Vector3(0, 0.73f, 0);

    private Spawn_Manager _spawn_Manager;
    private UIManager _uiManager;
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]
    private Moving_Asteroid _movingAsteroid;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _powerupSoundClip;
    private AudioSource _audioSourse;

    private bool _tripleShootActive = false;
    private bool _speedPowerupActive = false;
    private bool _shieldActive = false;




    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        _shield.gameObject.SetActive(false);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSourse = GetComponent<AudioSource>();

        _spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

        if (_spawn_Manager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if (_audioSourse == null)
        {
            Debug.LogError("The AudioSource on the player is NULL.");
        }
        else
        {
            _audioSourse.clip = _laserSoundClip;
        }

        _anim = GetComponent<Animator>();

        _currentLevel = 1;

        _enemy.enemyLevel = 1;

    }

    void Update()
    {
        CalculateMovement();
        CheckScore();
        DamageFX();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canshoot)
            FireLaser();

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_speedPowerupActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (_speed * _boostSpeed) * Time.deltaTime);
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 3), 0);

        if (transform.position.x > 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }

        if (horizontalInput < 0)
        {
            _anim.SetFloat("Move", -0.8f, 0.1f, Time.deltaTime);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetFloat("Move", 0.8f, 0.1f, Time.deltaTime);
        }
        else
        {
            _anim.SetFloat("Move", 0, 0.1f, Time.deltaTime);
        }

    }

    void FireLaser()
    {
        _canshoot = Time.time + _fireRate;

        if (_tripleShootActive == true)
        {
            Instantiate(_tripleShoot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + _offSet, Quaternion.identity);
        }

        _audioSourse.Play();

    }

    public void Damage()
    {
        if (_shieldActive == true)
        {
            _shieldActive = false;
            _shield.SetActive(false);
            return;
        }
        _lives -= 1;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawn_Manager.OnPlayerDeath();
            Instantiate(_explosionPref, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void DamageFX()
    {
        if (_lives == 3)
        {
            _lowDamage.SetActive(false);
            _highDamage.SetActive(false);
        }
        else if (_lives == 2)
        {
            _lowDamage.SetActive(true);
            _highDamage.SetActive(false);
        }
        else if (true)
        {
            _lowDamage.SetActive(false);
            _highDamage.SetActive(true);
        }
    }

    IEnumerator TripleShootRoutine()
    {
        _tripleShootActive = true;
        yield return new WaitForSeconds(5f);
        _tripleShootActive = false;
    }

    public void TripleShootPowerup()
    {
        if (_tripleShootActive == true)
        {
            StopCoroutine("TripleShootRoutine");
            StartCoroutine(TripleShootRoutine());
        }
        else
        {
            StartCoroutine(TripleShootRoutine());
        }
    }

    IEnumerator SpeedPowerupRoutine()
    {
        _speedPowerupActive = true;
        yield return new WaitForSeconds(5f);
        _speedPowerupActive = false;
    }

    public void SpeedPowerup()
    {
        if (_speedPowerupActive == true)
        {
            StopCoroutine("SpeedPowerupRoutine");
            StartCoroutine(SpeedPowerupRoutine());
        }
        else
        {
            StartCoroutine(SpeedPowerupRoutine());
        }
    }

    public void ShieldPowerup()
    {
        if (_shieldActive == true && _lives < 3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
        }
        else
        {
            _shieldActive = true;
            _shield.SetActive(true);
        }
    }

    public void AddToScore(float points)
    {

        _score += (points + (points * _currentLevel / 2));

        _uiManager.UpdateScore(_score);
    }

    public void CheckScore()
    {
        if (_score >= 300 && _score <= 400)
        {
            do
            {
                _currentLevel = 2;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 100;
                _enemy.enemyLevel = 2;
                _spawn_Manager.spawnManagerLevel = 2;
            } while (false);
        }
        else if (_score >=1000 && _score <= 1100)
        {
            do
            {
                _currentLevel = 3;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 100;
                _enemy.enemyLevel = 3;
            } while (false);

        }
        else if (_score >= 2500 && _score <= 2600)
        {
            do
            {
                _currentLevel = 4;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 100;
                _spawn_Manager.spawnManagerLevel = 3;
                _movingAsteroid.asteroidLevel = 1;
            } while (false);
        }
        else if (_score >= 5000 && _score <= 5120)
        {
            do
            {
                _currentLevel = 5;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 120;
                _enemy.enemyLevel = 4;
                _movingAsteroid.asteroidLevel = 2;
            } while (false);
        }
        else if (_score >= 8000 && _score <= 8140)
        {
            do
            {
                _currentLevel = 6;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 140;
                _spawn_Manager.spawnManagerLevel = 4;
                _movingAsteroid.asteroidLevel = 3;
            } while (false);
        }
        else if (_score >= 12000 && _score <= 12150)
        {
            do
            {
                _currentLevel = 7;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 150;
                _enemy.enemyLevel = 5;
                _movingAsteroid.asteroidLevel = 4;
            } while (false);
        }
        else if (_score >= 15000 && _score <= 15160)
        {
            do
            {
                _currentLevel = 8;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 160;
                _spawn_Manager.spawnManagerLevel = 4;
                _movingAsteroid.asteroidLevel = 5;
            } while (false);
        }
        else if (_score >= 20000 && _score <= 20170)
        {
            do
            {
                _currentLevel = 9;
                _uiManager.UpdateLevel(_currentLevel);
                _score += 170;
                _enemy.enemyLevel = 6;
            } while (false);
        }
        else if (_score >= 30000 & _score <= 30180)
        {
            do
            {
            _currentLevel = 10;
            _uiManager.UpdateLevel(_currentLevel);
                _score += 180;
            _spawn_Manager.spawnManagerLevel = 5;
            _movingAsteroid.asteroidLevel = 6;
            } while (false);
        }
        else if (_score >= 50000 & _score <= 50500)
        {
            do
            {
                _currentLevel = 11;
                _uiManager.FinalStageUpdate();
                _uiManager.winActivate = true;
                _score += 500;
                _spawn_Manager.spawnManagerLevel = 6;
            } while (false);
        }
    }
}
