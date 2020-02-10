using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPref;
    private float _speed;
    public int lives;
    public int asteroidLevel;
    private float _randomRotation;

    private Player _player;

    private void Start()
    {
        AsteroidLevel();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _randomRotation = Random.Range(-15f, 15f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(0, 0, _randomRotation * Time.deltaTime);

        if (transform.position.y < -8 )
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }
        else if (transform.position.y > 10)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, -8, 0);
        }
        else if (transform.position.x < -11)
        {
            float randomY = Random.Range(-8f, 8f);
            transform.position = new Vector3(11, randomY, 0);
        }
        else if (transform.position.x > 11)
        {
            float randomY = Random.Range(-8f, 8f);
            transform.position = new Vector3(-11, randomY, 0);
        }
    }

    public void AsteroidLevel()
    {
        switch (asteroidLevel)
        {
            case 1:
                _speed = 1f;
                lives = 4;
                break;
            case 2:
                _speed = 1.5f;
                lives = 4;
                break;
            case 3:
                _speed = 2.2f;
                lives = 3;
                break;
            case 4:
                _speed = 2.8f;
                lives = 3;
                break;
            case 5:
                _speed = 3.2f;
                lives = 2;
                break;
            case 6:
                _speed = 4f;
                lives = 2;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();


            if (player != null)
            {
                player.Damage();
            }
            GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            newExplosion.transform.localScale = transform.localScale;
            newExplosion.transform.rotation = transform.rotation;
            Destroy(this.gameObject);
        }
        if (other.tag == "Projectile")
        {

            Destroy(other.gameObject);
            if (lives > 1)
            {
                lives--;
                _player.AddToScore(10);
            }
            else if (lives <= 1)
            {
                if (_player != null)
                {
                    _player.AddToScore(30);

                }
                lives--;
                GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
                newExplosion.transform.localScale = transform.localScale;
                newExplosion.transform.rotation = transform.rotation;
                Destroy(this.gameObject);
            }

        }

        if (other.tag == "EnemyProjectile")
        {
            lives--;
            Destroy(other.gameObject);
        }
        else if (lives <= 1)
        {
            lives--;
            GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            newExplosion.transform.localScale = transform.localScale;
            newExplosion.transform.rotation = transform.rotation;
            Destroy(this.gameObject);
        }

        if (other.tag == "Asteroid")
        {
            GameObject newExplosion = Instantiate(_explosionPref, transform.position, Quaternion.identity);
            newExplosion.transform.localScale = transform.localScale;
            Destroy(this.gameObject);
        }
    }
}
