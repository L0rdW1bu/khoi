using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting2 : MonoBehaviour
{
    public GameObject projectile2;
    public Transform player2;
    public float minDamage2;
    public float maxDamage2;
    public float projectileForce2;
    public float cooldown2;

    void Start()
    {
        StartCoroutine(ShootPlayer2());
    }

    IEnumerator ShootPlayer2()
    {
        yield return new WaitForSeconds(cooldown2);
        if (player2 != null)
        {
            GameObject spell = Instantiate(projectile2, transform.position, Quaternion.identity);
            Vector2 myPos = transform.position;
            Vector2 targetPos = player2.position;
            Vector2 direction = (targetPos - myPos).normalized;

            
            var projectileScript = spell.GetComponent<TestEnemyProjectiles2>();
            projectileScript.damage = Random.Range(minDamage2, maxDamage2);
            projectileScript.target = player2; 
            projectileScript.moveSpeed = projectileForce2;

            StartCoroutine(ShootPlayer2());
        }
    }
}
