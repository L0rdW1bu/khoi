using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;
    private float time = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name != "Player")
        {
            if(collision.GetComponent<EnemyRecieveDamage>() !=null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);

            }
            else if (collision.GetComponent<EnemyRecieveDamage2>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage2>().DealDamage(damage);

            }
            Destroy(gameObject);
        }
    }
    void Update()
    {
        time++;
        if( time * Time.deltaTime > 10)
        {
            Destroy(gameObject);
        }
    }
}
