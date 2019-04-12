using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Stats")]
    public float range;
    public int attackDamage;
    public float hitboxX;
    public float hitboxY;
    
    [Header ("Attack Positions")]
    public Transform attackDownPos;
    public Transform attackLeftPos;
    public Transform attackUpPos;
    public Transform attackRightPos;

    private int attackDirection;
    private float playerDistance;
    private float attackDelay;
    private Vector2 hitbox;

    private Animator animator;
    private GameObject player;
    private LayerMask playerLayerMask;

    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
        playerLayerMask = LayerMask.GetMask("Player");

        animator = GetComponent<Animator>();
        hitbox = new Vector2(hitboxX, hitboxY);
        attackDelay = 0.2f;
    }


    void Update() {
        playerDistance = GetDistanceToPlayer();
        if(playerDistance <= range) {
            Invoke("Attack", attackDelay);
        }
      
    }

    private float GetDistanceToPlayer() {

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
            if (minDistance > distances[i]) {
                minDistance = distances[i];
                attackDirection = i + 1;
            }

        return minDistance;
    }

    private void Attack() {
        Collider2D[] playerCol = null;
        switch (attackDirection)
        {
            case 1:
                playerCol = Physics2D.OverlapBoxAll(attackDownPos.position, hitbox, 0f, playerLayerMask);
                animator.SetFloat("AttackDirection", 1f);
                break;
            case 2:
                playerCol = Physics2D.OverlapBoxAll(attackLeftPos.position, hitbox, 0f, playerLayerMask);
                animator.SetFloat("AttackDirection", 2f);
                break;
            case 3:
                playerCol = Physics2D.OverlapBoxAll(attackUpPos.position, hitbox, 0f, playerLayerMask);
                animator.SetFloat("AttackDirection", 3f);
                break;
            case 4:
                playerCol = Physics2D.OverlapBoxAll(attackRightPos.position, hitbox, 0f, playerLayerMask);
                animator.SetFloat("AttackDirection", 4f);
                break;
            default:
                break;
        }
        if (playerCol.Length > 0) {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
        animator.SetTrigger("Attack");
    }
}
