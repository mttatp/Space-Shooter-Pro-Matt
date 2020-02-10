using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int _powerupID;

    [SerializeField]
    private AudioClip _audioClip;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);

            {

                player.AddToScore(40);
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShootPowerup();
                        break;
                    case 1:
                        player.SpeedPowerup();
                        break;
                    case 2:
                        player.ShieldPowerup();
                        break;
                }

                Destroy(this.gameObject);
            }
        }
        if (other.tag == "Projectile")
        {
            Destroy(this.gameObject);
        }
    }
}