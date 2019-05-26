using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player Stats")]
    public float speed;
    public float health;
    public float iFrames;
    public float knockbackStrength;
    
    [Header ("Attack Positions")]
    public Transform attackUpPos;
    public Transform attackDownPos;
    public Transform attackLeftPos;
    public Transform attackRightPos;

    private SpriteRenderer rend;
    private Rigidbody2D rb;
    private Animator animator;
    private Camera cam;
    private Weapon weapon;

    private bool isInvincible;
    private int attackDirection,weaponDamage;
    private LayerMask enemyLayerMask;
    private Vector2 mousePos, movementInput, movement;
    private float iFrameTime,timeBtwAttack, startTimeBtwAttack, animWepIndex, attackRange;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<Weapon>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
        isInvincible = false;
        timeBtwAttack = 0f;
        startTimeBtwAttack = 0.55f;
        animWepIndex = weapon.animatorIndex;
        weaponDamage = weapon.damage;
        attackRange = weapon.range;
        knockbackStrength += weapon.knockback;
        enemyLayerMask = LayerMask.GetMask("Enemy");

       
    }

    private void Update() {
        GetInput();
        IFrameControl();
        SetAnimatorMovementParams();
        Attack();
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
        for (int i = 1; i < 4; ++i)
            if (minDistance > distances[i]) {
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
            bool attacked = false;

            if(Input.GetButtonDown("Fire1")) {

                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                switch (attackDirection)
                {
                    case 1:
                        StartCoroutine(SpawnAttackCircle(attackDownPos.position));
                        animator.SetFloat("AttackDirection", 1f);
                        break;
                    case 2:
                        StartCoroutine(SpawnAttackCircle(attackLeftPos.position));
                        animator.SetFloat("AttackDirection", 2f);
                        break;
                    case 3:
                        StartCoroutine(SpawnAttackCircle(attackUpPos.position));
                        animator.SetFloat("AttackDirection", 3f);
                        break;
                    case 4:
                        StartCoroutine(SpawnAttackCircle(attackRightPos.position));
                        animator.SetFloat("AttackDirection", 4f);
                        break;
                    default:
                        break;
                }
                

            }

            if(!attacked && Input.GetKeyDown(KeyCode.DownArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 1f);

                StartCoroutine(SpawnAttackCircle(attackDownPos.position));

            }

            if (!attacked && Input.GetKeyDown(KeyCode.LeftArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 2f);

                StartCoroutine(SpawnAttackCircle(attackLeftPos.position));
              
            }

            if (!attacked && Input.GetKeyDown(KeyCode.UpArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 3f);

                StartCoroutine(SpawnAttackCircle(attackUpPos.position));

            }

            if (!attacked && Input.GetKeyDown(KeyCode.RightArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
               
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 4f);


                StartCoroutine(SpawnAttackCircle(attackRightPos.position));
                
            }
            
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    IEnumerator SpawnAttackCircle(Vector2 position)
    {
        yield return new WaitForSeconds(0.15f);
        Collider2D[] enemies = null;
        enemies = Physics2D.OverlapCircleAll(position, attackRange, enemyLayerMask);

        for (int i = 0; i < enemies.Length; ++i)
        {
            enemies[i].GetComponent<Enemy>().HitByPlayer(weaponDamage, knockbackStrength, new Vector2(transform.position.x, transform.position.y));
        }
    }


    private void IFrameControl(){
        if (isInvincible) {
            iFrameTime -= Time.deltaTime;
            if(iFrameTime <= 0f) {
                isInvincible = false;
                Color color = rend.material.color;
                color.a = 1f;
                rend.material.color = color;
            }
        }
    }

    public void TakeDamage(int damage) {
        if(!isInvincible) {

            health -= damage;
            isInvincible = true;
            iFrameTime = iFrames;

            //GetComponentInChildren<PlayerDamage>().DisableCollider();

            Color color = rend.material.color;
            color.a = 0.5f;
            rend.material.color = color;
        }
        
           
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackDownPos.position, attackRange);
        Gizmos.DrawWireSphere(attackUpPos.position, attackRange);
        Gizmos.DrawWireSphere(attackLeftPos.position, attackRange);
        Gizmos.DrawWireSphere(attackRightPos.position, attackRange);

    }
}
