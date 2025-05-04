using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestEnemyProjectiles2 : MonoBehaviour
{
    public float damage;
    public Transform target; 
    public float rotateSpeed = 200f; 
    public float moveSpeed = 5f;
    private float time = 0;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag != "Enemy" && collision.tag != "Projectile")
       {
                if (collision.tag == "Player")
            {
                PlayerStats.playerStats.DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed ;
        rb.velocity = transform.up * moveSpeed ;
        time++;
        if(time * Time.deltaTime > 8)
        {
            Destroy(gameObject);
        }
    }
}