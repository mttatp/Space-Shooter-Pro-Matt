using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12f;

    private void Update()
    {
        transform.Translate(Vector3.up * -_speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
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
            else
            {
                Debug.LogError("The Player on Enemy Laser is NULL.");
            }

            Destroy(this.gameObject);
        }
        else if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
