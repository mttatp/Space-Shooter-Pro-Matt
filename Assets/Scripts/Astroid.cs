using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private GameObject _explosionPref;
    [SerializeField]
    private int _asteroidLives = 3;
    private Spawn_Manager _spawnManager;
    private UIManager _uiManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {

            Destroy(other.gameObject);
            _asteroidLives--;

            if (_asteroidLives == 0)
            {
                Instantiate(_explosionPref, this.transform.position, Quaternion.identity);
                _uiManager.UpdateLevel(1);
                _spawnManager.StartSpawning();
                _uiManager._instructionsImg.gameObject.SetActive(false);
                Destroy(this.gameObject, 0.25f);
            }

        }
    }


    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }
}
