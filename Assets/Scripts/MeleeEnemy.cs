using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;
    public float chaseRange = 6f;
    public float attackCooldown = 1f;
    public int damage = 10;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private float lastAttackTime;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            animator.SetBool("isRunning", false);
            TryAttack();
        }
        else if (distance <= chaseRange)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);

            FlipTowardsPlayer();  

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void FlipTowardsPlayer()
    {
        
        if (player.position.x < transform.position.x) 
        {
            transform.localScale = new Vector3(-5, 5, 1);  
        }
        else  
        {
            transform.localScale = new Vector3(5, 5, 1);   
        }
    }

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");  
            FlipTowardsPlayer();  
        }
    }

    
    public void DealDamageEvent()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerStats.playerStats.DealDamage(damage);  
        
        }
    }

  
    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        FlipTowardsPlayer();
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
