using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float attackRange = 10f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Transform targetEnemy;  
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (targetEnemy == null) return;

        float distance = Vector3.Distance(transform.position, targetEnemy.position);

       
        if (distance > attackRange)
        {
            agent.SetDestination(targetEnemy.position);
        }
        else
        {
           
            agent.ResetPath();
            transform.LookAt(targetEnemy);  
            if (Time.time > lastAttackTime + attackCooldown)
            {
                Shoot();
                lastAttackTime = Time.time;
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        targetEnemy = newTarget;
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (targetEnemy.position - shootPoint.position).normalized;
                rb.velocity = direction * 20f; 
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetTargetTag("Enemy");  
            }
        }
        else
        {
            
        }
    }
}