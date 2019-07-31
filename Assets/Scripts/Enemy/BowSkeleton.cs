using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkeleton : MonoBehaviour
{
    [Header("Stats")]
    public float attackRange;
    public float runRange;
    public int collisionDamage;
    public float attackSpeed;
    public float speed;
    public float animationDelay;

    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    [Header("Arrow")]
    public GameObject arrow;
    public float arrowSpeed;

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject player;

    private float attackTimer;
    private float distanceToPlayer;
    private int attackDirection;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        attackTimer = 0f;
    }

    
    void FixedUpdate() {
        GetDistanceToPlayer();
        TickAttackTimer();
        Move();
    }

    private void Move() {
        if (distanceToPlayer <= runRange)
        {
            rb.AddForce(-(player.transform.position - transform.position).normalized * speed );
            if (attackDirection + 2 > 4) animator.SetFloat("MoveDirection", (attackDirection + 2) % 4);
            else animator.SetFloat("MoveDirection", attackDirection);
            animator.SetBool("IsMoving", true);
        }
        if (distanceToPlayer > runRange && distanceToPlayer <= attackRange)
        {
            animator.SetBool("IsMoving", false);
            if (attackTimer <= 0f) {
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", attackDirection);
                Invoke("Attack", animationDelay);
                attackTimer = attackSpeed;
            }
        }
        if (distanceToPlayer > attackRange)
        {
            rb.AddForce( (player.transform.position - transform.position).normalized * speed );
            animator.SetFloat("MoveDirection", attackDirection);
            animator.SetBool("IsMoving", true);
        }
    }

    private void Attack() {
        Rigidbody2D arrowRb = null;
        
        if(attackDirection == 1) {
            arrowRb = (Instantiate(arrow, attackDown.transform.position,  Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        if (attackDirection == 2) {
            arrowRb = (Instantiate(arrow, attackLeft.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        if (attackDirection == 3) {
            arrowRb = (Instantiate(arrow, attackUp.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        if (attackDirection == 4) {
            arrowRb = (Instantiate(arrow, attackRight.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }



        Vector3 targetPosition = new Vector3(player.transform.position.x - arrowRb.transform.position.x, player.transform.position.y - arrowRb.transform.position.y, 0f);


        arrowRb.transform.up = targetPosition;
        arrowRb.AddForce((player.transform.position - transform.position).normalized * arrowSpeed);
    }

    private void GetDistanceToPlayer() {
        float minDistance;

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        distanceToPlayer = Vector2.Distance(playerPosition, transform.position);

        float[] distances = {
                    Vector2.Distance(playerPosition, attackDown.position),
                    Vector2.Distance(playerPosition, attackLeft.position),
                    Vector2.Distance(playerPosition, attackUp.position),
                    Vector2.Distance(playerPosition, attackRight.position)
        };


        minDistance = distances[0];

        attackDirection = 1;

        for (int i = 1; i < 4; ++i)
            if (minDistance > distances[i])
            {

                minDistance = distances[i];
                attackDirection = i + 1;
            }
    }


    private void TickAttackTimer() {
        if(attackTimer > 0f ) attackTimer -= Time.deltaTime;
    }
}