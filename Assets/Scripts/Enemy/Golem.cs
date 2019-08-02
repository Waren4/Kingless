using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{



    [Header("Stats")]
    public float speed;
    public float collisionDamage;
    public float range;
    public float attackSpeed;
    public float animationDelay;


    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;


    [Header("Shockwave")]
    public GameObject shockwave;


    private Rigidbody2D rb;
    private Animator animator;
    private GameObject player;


    private float distanceToPlayer;
    private float attackTimer;
    private int attackDirection;
    private float waitForAttackEnd = 0.95f;
    private float waitTimer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        attackTimer = 0f;
        waitTimer = 0;
    }

    void FixedUpdate() {
        GetDistanceToPlayer();
        TickAttackTimer();
        TickWaitTimer();
        Move();
    }


    private void Move() {
        if(distanceToPlayer > range && waitTimer <= 0f) {
            rb.AddForce((player.transform.position - transform.position).normalized * speed);
            animator.SetFloat("MoveDirection", attackDirection);
            animator.SetBool("IsMoving", true);
        }
        else {
            if (attackTimer <= 0f) {
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", attackDirection);
                animator.SetFloat("MoveDirection", attackDirection);
                animator.SetBool("IsMoving", false);
                Invoke("Attack", animationDelay);
                attackTimer = attackSpeed;
                waitTimer = waitForAttackEnd;
            }
        }
    }


    private void Attack() {
        if(attackDirection == 1) {
            Instantiate(shockwave, attackDown.position, Quaternion.identity);
        }
        if (attackDirection == 2) {
            Instantiate(shockwave, attackLeft.position, Quaternion.identity);
        }
        if (attackDirection == 3) {
            Instantiate(shockwave, attackUp.position, Quaternion.identity);
        }
        if (attackDirection == 4) {
            Instantiate(shockwave, attackRight.position, Quaternion.identity);
        }

    }

    private void GetDistanceToPlayer()
    {
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
        if (attackTimer > 0f) attackTimer -= Time.deltaTime;
    }

    private void TickWaitTimer() {
        if (waitTimer> 0f) waitTimer -= Time.deltaTime;
    }
}
