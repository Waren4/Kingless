﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    [Header("Attack Stats")]
    public float range;
    public int attackDamage;
    public float hitboxX;
    public float hitboxY;
    public int collisionDamage;
    public float attackSpeed;

    [Header("Movement")]
    public float speed;
    public float startTimeBtwMoves;
    public float moveDistance;
    public char color;

    [Header("Attack Positions")]
    public Transform attackDownPos;
    public Transform attackLeftPos;
    public Transform attackUpPos;
    public Transform attackRightPos;

    private int attackDirection;
    private float playerDistance;
    private float attackDelay;
    private Vector2 hitboxVert,hitboxHor;

    private Rigidbody2D rb;
    private GameObject player;
    private LayerMask playerLayerMask;
    private Animator animator;
    private Vector2 moveDirection;
    private float timeBetweenMoves;
    private float timeBetweenAttacks;


    void Start() {
        timeBetweenMoves = Random.Range(1f,3f);
        timeBetweenAttacks = 0f;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerLayerMask = LayerMask.GetMask("EnemyCollisions");

        hitboxVert = new Vector2(hitboxX, hitboxY);
        hitboxHor = new Vector2(hitboxY, hitboxX);

        attackDelay = 0.4f;
    }

    
    void FixedUpdate() {
        TickAttackTimer();
        if (timeBetweenMoves <= 0f)
        {
            GetRandomDirection();
            StartCoroutine(Move(moveDirection));
            timeBetweenMoves = startTimeBtwMoves;
        }
        else {
             if (color == 'B') {
                if (timeBetweenAttacks <= 0f)
                {
                    playerDistance = GetDistanceToPlayer();
                    if (playerDistance <= range)
                    {
                        animator.SetTrigger("Attack");
                        animator.SetFloat("AttackDirection", (float) attackDirection);
                        animator.SetFloat("MoveDirection", (float) attackDirection);
                        timeBetweenAttacks = attackSpeed;
                        Invoke("Attack", attackDelay);
                        Invoke("TickMoveTimer", attackDelay);
                    }
                    else TickMoveTimer();
                }
                else TickMoveTimer();
                }
             else TickMoveTimer();
        }
    }

    private void TickAttackTimer(){
        if (timeBetweenAttacks > 0f) timeBetweenAttacks -= Time.deltaTime;
    }

    private void TickMoveTimer() {
        timeBetweenMoves -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);
            
        }
    }

    private void GetRandomDirection()
    {
        int ranDirection = Random.Range(1, 5);

        moveDirection = new Vector2(0f, 0f);
        switch (ranDirection)
        {
            case 1:
                if(color == 'G' || color == 'B')
                {
                    moveDirection.y = -1f;
                }
                if(color == 'R')
                {
                    moveDirection.y = -1f;
                    moveDirection.x = -1f;
                }
                animator.SetFloat("MoveDirection", 1f);
                break;
            case 2:
                if (color == 'G' || color == 'B')
                {
                    moveDirection.x = -1f;
                }
                if (color == 'R')
                {
                    moveDirection.y = 1f;
                    moveDirection.x = -1f;
                }
                animator.SetFloat("MoveDirection", 2f);
                break;
            case 3:
                if (color == 'G' || color == 'B')
                {
                    moveDirection.y = 1f;
                }
                if (color == 'R')
                {
                    moveDirection.y = 1f;
                    moveDirection.x = 1f;
                }
                animator.SetFloat("MoveDirection", 3f);
                break;
            case 4:
                if (color == 'G' || color == 'B')
                {
                    moveDirection.x = 1f;
                }
                if (color == 'R')
                {
                    moveDirection.y = -1f;
                    moveDirection.x = 1f;
                }
                animator.SetFloat("MoveDirection", 4f);
                break;
            default:
                break;
        }

    }

    IEnumerator Move(Vector2 direction)
    {
        float moveTime = 0.65f;
        while(moveTime >= 0f) {
            animator.SetBool("IsMoving", true);
            rb.AddForce(direction.normalized * speed );
            yield return new WaitForFixedUpdate();
            moveTime -= Time.deltaTime;
            
        }

        animator.SetBool("IsMoving", false);
    }

    private float GetDistanceToPlayer()
    {

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        float[] distances = {
                    Vector2.Distance(playerPosition, attackDownPos.position),
                    Vector2.Distance(playerPosition, attackLeftPos.position),
                    Vector2.Distance(playerPosition, attackUpPos.position),
                    Vector2.Distance(playerPosition, attackRightPos.position)
        };
        float minDistance = distances[0];
        attackDirection = 1;
        for (int i = 1; i < 4; ++i)
            if (minDistance > distances[i])
            {
                minDistance = distances[i];
                attackDirection = i + 1;
            }

        return minDistance;
    }

    private void Attack()
    {
        Collider2D[] playerCol = null;
        switch (attackDirection)
        {
            case 1:
                playerCol = Physics2D.OverlapBoxAll(attackDownPos.position, hitboxVert, 0f, playerLayerMask);
                
                break;
            case 2:
                playerCol = Physics2D.OverlapBoxAll(attackLeftPos.position, hitboxHor, 0f, playerLayerMask);
                
                break;
            case 3:
                playerCol = Physics2D.OverlapBoxAll(attackUpPos.position, hitboxVert, 0f, playerLayerMask);
                
                break;
            case 4:
                playerCol = Physics2D.OverlapBoxAll(attackRightPos.position, hitboxHor, 0f, playerLayerMask);
                
                break;
            default:
                break;
        }
        if (playerCol.Length > 0)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(attackDownPos.position, hitboxVert);
        Gizmos.DrawWireCube(attackUpPos.position, hitboxVert);
        Gizmos.DrawWireCube(attackLeftPos.position, hitboxHor);
        Gizmos.DrawWireCube(attackRightPos.position, hitboxHor);
        

    }
}
