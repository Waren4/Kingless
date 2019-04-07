using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed;

    public Transform attackUpPos, attackDownPos, attackLeftPos, attackRightPos;
    public Vector2 velocity;
    private Rigidbody2D rb;
    private Animator animator;
    private Camera cam;

    private int attackDirection,weaponDamage;
    private LayerMask enemyLayerMask;
    private Vector2 mousePos, movementInput, movement;
    private float timeBtwAttack, startTimeBtwAttack, animWepIndex, attackRange;


    private void Start() { 
    
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        timeBtwAttack = 0f;
        startTimeBtwAttack = 0.55f;
        animWepIndex = gameObject.GetComponentInChildren<Weapon>().animatorIndex;
        weaponDamage = gameObject.GetComponentInChildren<Weapon>().damage;
        attackRange = gameObject.GetComponentInChildren<Weapon>().range;
        enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void Update() {
        GetInput();
        SetAnimatorMovementParams();
        Attack();
        velocity = rb.velocity;
    }

    private void FixedUpdate() {
        Move();
    }

    private void GetInput() {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput.Normalize();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float[] distances = {
                    Vector2.Distance(mousePos, attackDownPos.position),
                    Vector2.Distance(mousePos, attackLeftPos.position),
                    Vector2.Distance(mousePos, attackUpPos.position),
                    Vector2.Distance(mousePos, attackRightPos.position)
        };
        float minDistance = distances[0];
        attackDirection = 1;
        for (int i = 1; i < 4; ++i) if (minDistance > distances[i]) {
                minDistance = distances[i];
                attackDirection = i + 1;
            }
    }

    private void SetAnimatorMovementParams() {
        if (movement.magnitude > 0) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Magnitude", movementInput.magnitude);
    }

    private void Move()
    {
        movement = movementInput * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
    
    private void Attack() {
        if (timeBtwAttack <= 0f) {
            if(Input.GetButtonDown("Fire1")) {

                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemies = null;
                animator.SetTrigger("Attack");
                switch (attackDirection)
                {
                    case 1:
                        enemies = Physics2D.OverlapCircleAll(attackDownPos.position, attackRange, enemyLayerMask);
                        animator.SetFloat("AttackDirection", 1f);
                        break;
                    case 2:
                        enemies = Physics2D.OverlapCircleAll(attackLeftPos.position, attackRange, enemyLayerMask);
                        animator.SetFloat("AttackDirection", 2f);
                        break;
                    case 3:
                        enemies = Physics2D.OverlapCircleAll(attackUpPos.position, attackRange, enemyLayerMask);
                        animator.SetFloat("AttackDirection", 3f);
                        break;
                    case 4:
                        enemies = Physics2D.OverlapCircleAll(attackRightPos.position, attackRange, enemyLayerMask);
                        animator.SetFloat("AttackDirection", 4f);
                        break;
                    default:
                        break;
                }
                for(int i = 0; i < enemies.Length; ++i) {
                    enemies[i].GetComponent<Enemy>().TakeDamage(weaponDamage);
                }

            }
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackDownPos.position, attackRange);
    }
}
