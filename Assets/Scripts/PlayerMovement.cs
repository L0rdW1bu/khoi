using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private Vector2 dashDir;
    public float cost;
    public float dashRange;
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private int dashFrameCounter = 0;
    private int playerLayer;
    private int enemyLayer;
    private int projectileLayer;



    private enum Facing  {UP,DOWN,LEFT,RIGHT};
    private Facing FacingDir = Facing.LEFT;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        projectileLayer = LayerMask.NameToLayer("Projectile");

    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            if (dashFrameCounter == 0)
            {
                int wallMask = LayerMask.GetMask("Wall");
                RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDir, dashRange, wallMask);
                Debug.DrawRay(rb.position, dashDir * dashRange, Color.red);

                Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
                Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, true);

                if (hit.collider == null)
                {
                    rb.MovePosition(rb.position + dashDir * dashRange);
                }
                else
                {
                    float distanceToWall = hit.distance;
                    rb.MovePosition(rb.position + dashDir * distanceToWall);
                }

                dashFrameCounter++;
                return;
            }
            else
            {
                Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
                Physics2D.IgnoreLayerCollision(playerLayer, projectileLayer, false);

                isDashing = false;
                dashFrameCounter = 0;
                return;
            }
        }

        Move();
    }

    private void Move()
    {
        if (direction != Vector2.zero)
        {
            Vector2 newPos = rb.position + direction.normalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
    private Vector2 FacingToVector2(Facing face)
    {
        switch (face)
        {
            case Facing.UP: return Vector2.up;
            case Facing.DOWN: return Vector2.down;
            case Facing.LEFT: return Vector2.left;
            case Facing.RIGHT: return Vector2.right;
            default: return Vector2.zero;
        }
    }
    void Update()
    {
        TakeInput();

       
    }

    void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            FacingDir = Facing.UP;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            FacingDir = Facing.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            FacingDir = Facing.DOWN;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            FacingDir = Facing.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.Space) && PlayerStats.playerStats.mana >= cost)
        {
            PlayerStats.playerStats.UseSkill(cost);
            dashDir = direction != Vector2.zero ? direction.normalized : FacingToVector2(FacingDir);
            isDashing = true;
            dashFrameCounter = 0;
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);

    }
    

}
