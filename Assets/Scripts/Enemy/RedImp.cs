using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedImp : MonoBehaviour
{

    [Header("Attack Stats")]
    public float range;
    public int attackDamage;
    public float hitboxX;
    public float hitboxY;
    public int collisionDamage;
    public float attackSpeed;
    public float animationDelay;

    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    [Header("Movement")]
    public float speed;
    public float normalSpeed;
    public float followSpeed;
    public float startTimeBtwMoves;
    public float startMoveTime;
    public float moveTime;
    public bool isFollowing;
    public bool isMoving;

    [Header("Vision")]
    public Transform visionTransform;
    public float timeBetweenMoves;

    [Header("Slash Effect")]
    public GameObject slashEffect;

    
    private float timeBetweenAttacks;
    private float attackDirection;

    private Quaternion[] rotations;
    private Vector2 direction;
    private Vector2 hitboxVert, hitboxHor;
    private Rigidbody2D rb;
    private GameObject player;
    private LayerMask playerLayerMask;
    private Animator animator;
    private Queue<Vector3> q;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerLayerMask = LayerMask.GetMask("EnemyCollisions");

        hitboxVert = new Vector2(hitboxX, hitboxY);
        hitboxHor = new Vector2(hitboxY, hitboxX);

        timeBetweenMoves = startTimeBtwMoves;
        timeBetweenAttacks = attackSpeed;

        rotations = new Quaternion[5];

        rotations[0] = Quaternion.Euler(0f, 0f, 0f);
        rotations[1] = Quaternion.Euler(0f, 0f, 0f);
        rotations[2] = Quaternion.Euler(0f, 0f, 270f);
        rotations[3] = Quaternion.Euler(0f, 0f, 180f);
        rotations[4] = Quaternion.Euler(0f, 0f, 90f);

        direction = new Vector3(0f, -1f);
        speed = normalSpeed;

        q = new Queue<Vector3>();
        q.Enqueue(direction);
    }

   
    void FixedUpdate() {

        TickAttackTimer();

        if (isFollowing) {
            if (GetDistanceToPlayer() <= range) {

                animator.SetBool("IsMoving", false);
                if (timeBetweenAttacks <= 0f) {

                    timeBetweenAttacks = attackSpeed;
                    animator.SetTrigger("Attack");
                    animator.SetFloat("AttackDirection", attackDirection);
                    animator.SetFloat("MoveDirection", attackDirection);
                    visionTransform.rotation = rotations[(int)attackDirection];
                    Invoke("Attack", animationDelay);
                    
                }
            }
            else{
                Follow();
            }
            
        }
        else {
            if(isMoving) {
                if(moveTime > 0f) {
                    Move();
                }
                else{
                    animator.SetBool("IsMoving", false);
                    isMoving = false;
                    timeBetweenMoves = startTimeBtwMoves;
                }
            }
            else {
                if(timeBetweenMoves > 0f) {
                    TickStayTimer();
                }
                else {
                    ChangeDirection();
                    animator.SetBool("IsMoving", true);
                    isMoving = true;
                }
            }
        }
    }

    private void Attack() {
        Collider2D[] playerCol = null;
        switch (attackDirection)
        {
            case 1:
                playerCol = Physics2D.OverlapBoxAll(attackDown.position, hitboxVert, 0f, playerLayerMask);
                Instantiate(slashEffect, attackDown.position, Quaternion.Euler(0f, 0f, 270f));

                break;
            case 2:
                playerCol = Physics2D.OverlapBoxAll(attackLeft.position, hitboxHor, 0f, playerLayerMask);
                Instantiate(slashEffect, attackLeft.position, Quaternion.Euler(0f, 0f, 180f));

                break;
            case 3:
                playerCol = Physics2D.OverlapBoxAll(attackUp.position, hitboxVert, 0f, playerLayerMask);
                Instantiate(slashEffect, attackUp.position, Quaternion.Euler(0f, 0f, 90f));

                break;
            case 4:
                playerCol = Physics2D.OverlapBoxAll(attackRight.position, hitboxHor, 0f, playerLayerMask);
                Instantiate(slashEffect, attackRight.position, Quaternion.Euler(0f, 0f, 0f));

                break;
            default:
                break;
        }
        if (playerCol.Length > 0)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
            /*
            isFollowing = false;
            speed = normalSpeed;
            animator.SetBool("IsMoving", false);
            isMoving = false;
            timeBetweenMoves = startTimeBtwMoves;
            */
            
        }

        
    }

    private void Follow() {
        Vector2 moveDirection;
        animator.SetBool("IsMoving", true);
        animator.SetFloat("MoveDirection", attackDirection);
        visionTransform.rotation = rotations[(int)attackDirection];

        moveDirection = player.transform.position - transform.position;
        moveDirection.Normalize();
        rb.AddForce(moveDirection * speed);
    }

    private void Move() {
        TickMoveTimer();

        rb.AddForce(direction * speed);
    }

    private float GetDistanceToPlayer() {
        float minDistance;

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        float[] distances = {
                    Vector2.Distance(playerPosition, attackDown.position),
                    Vector2.Distance(playerPosition, attackLeft.position),
                    Vector2.Distance(playerPosition, attackUp.position),
                    Vector2.Distance(playerPosition, attackRight.position)
        };

        minDistance = distances[0];

        attackDirection = 1;

        for (int i = 1; i < 4; ++i)
            if (minDistance > distances[i]) {

                minDistance = distances[i];
                attackDirection = i + 1;
            }

        return minDistance;
    }

    private void ChangeDirection() {

        Vector3 dir = direction;

        if (q.Count == 4)
        {
            q.Clear();
        }

        while (q.Contains(dir))
        {
            GetRandomDirection();
            dir = direction;
        }

        q.Enqueue(direction);

        moveTime = startMoveTime;
    }

    private void GetRandomDirection()
    {
        int ranDirection = Random.Range(1, 5);

        direction = new Vector3(0f, 0f);
        switch (ranDirection)
        {
            case 1:
                direction.y = -1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                animator.SetFloat("MoveDirection", 1f);
                break;
            case 2:
                direction.x = -1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                animator.SetFloat("MoveDirection", 2f);
                break;
            case 3:
                direction.y = 1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                animator.SetFloat("MoveDirection", 3f);
                break;
            case 4:
                direction.x = 1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                animator.SetFloat("MoveDirection", 4f);
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);
            speed = normalSpeed;
            
            
        }
    }

    /*
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {

            ChangeDirection();
            animator.SetBool("IsMoving", true);
            isMoving = true;
        }
    }
    */

    private void TickAttackTimer() {
        if (timeBetweenAttacks > 0f) timeBetweenAttacks -= Time.deltaTime;
    }

    private void TickMoveTimer() {
        moveTime -= Time.deltaTime;
    }

    private void TickStayTimer(){
        timeBetweenMoves -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(attackDown.position, hitboxVert);
        Gizmos.DrawWireCube(attackUp.position, hitboxVert);
        Gizmos.DrawWireCube(attackLeft.position, hitboxHor);
        Gizmos.DrawWireCube(attackRight.position, hitboxHor);


    }

}
