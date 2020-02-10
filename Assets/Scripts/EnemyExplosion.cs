using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{

    void Start()
    {
        transform.Translate(Vector3.down * 4 * Time.deltaTime);
        Destroy(this.gameObject, 3f);
    }

}