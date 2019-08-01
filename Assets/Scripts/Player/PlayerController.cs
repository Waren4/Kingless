using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static float baseHealth = 100;
    public static float maxHealth = 100;
    public static int damageBoost = 0;
    public static int roomsCleared = 0;

    public static PlayerController playerInstance;

    [Header ("Player Stats")]
    public float baseSpeed;
    public float health;
    public float iFrames;
    public float knockbackStrength;
    public float dashCooldown;
    public float dashSpeed;

    [Header ("Attack Positions")]
    public Transform attackUpPos;
    public Transform attackDownPos;
    public Transform attackLeftPos;
    public Transform attackRightPos;

    [Header("Dash Animation")]
    public GameObject dashAnimation;

    [Header("Death Animation")]
    public GameObject deathAnimation;

    [Header("Hit Animation")]
    public GameObject hitAnimation;

    [Header("Hit Sound")]
    public GameObject hitSound;

    private SpriteRenderer rend;
    private Rigidbody2D rb;
    private Animator animator;
    private Camera cam;
    private Weapon weapon;

    private float speed;
    private bool isInvincible;
    private bool canDash;
    private int attackDirection,weaponDamage;
    private LayerMask enemyLayerMask;
    private Vector2 mousePos, movementInput, movement;
    private float iFrameTime,timeBtwAttack, startTimeBtwAttack, animWepIndex, attackRange, timeUntilDash;
    private Vector3 dashRotation;

    private void Awake() {
        DestroyOtherPlayerObjects();
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        weapon = GetComponentInChildren<Weapon>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        enemyLayerMask = LayerMask.GetMask("Enemy");

        isInvincible = false;


        timeBtwAttack = 0f;
        startTimeBtwAttack = 0.55f;

        animWepIndex = weapon.animatorIndex;
        attackRange = weapon.range;
        knockbackStrength += weapon.knockback;

        SetStats();
        SetDash();
    }

    private void Update() {
        
        GetInput();
        IFrameControl();
        Attack();
    }

    private void FixedUpdate() {
        Move();
        if(canDash) Dash();
    }


    private void GetInput() {

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        SetDashAnimationRotation();
   //     if (movementInput.x < 0) movementInput.x = -1;
   //     if (movementInput.x > 0) movementInput.x = 1;
   //     if (movementInput.y < 0) movementInput.y = -1;
   //     if (movementInput.y > 0) movementInput.y = 1;

        movementInput.Normalize();
        //     Debug.Log(movementInput);
        //     movement = movementInput * speed;

        if (cam == null) cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

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

    public void SetDash() {
        if (PlayerPrefs.GetInt("HasDash", 0) == 1) canDash = true;
        else canDash = false;
    }

    private void SetAnimatorMovementParams() {

        if (movement.magnitude > 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Magnitude", movementInput.magnitude);
    }

    private void Move()
    {
        movement = movementInput * speed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
            
        if (movementInput.magnitude == 0) animator.SetBool("IsMoving", false);
                                    else animator.SetBool("IsMoving", true);
        

        SetAnimatorMovementParams();
    }
    
    private void Dash() {

        if (timeUntilDash > 0f) timeUntilDash -= Time.deltaTime;

        if (movementInput.magnitude != 0) {
            if(timeUntilDash <= 0f && Input.GetAxis("Jump") != 0f) {
               // SetDashAnimationRotation();
                StartCoroutine(IncreaseSpeed());
                Instantiate(dashAnimation, transform.position+Vector3.down, Quaternion.Euler(dashRotation));
                timeUntilDash = dashCooldown;
            } 
        }
    }

    private void SetDashAnimationRotation() {
        if (movementInput.x == -1f && movementInput.y == -1f) dashRotation = new Vector3(0f, 0f, 5f);
        if (movementInput.x == 0f && movementInput.y == -1f) dashRotation = new Vector3(0f, 0f, 50f);
        if (movementInput.x == 1f && movementInput.y == -1f) dashRotation = new Vector3(0f, 0f, 95f);
        if (movementInput.x == 1f && movementInput.y == 0f) dashRotation = new Vector3(0f, 0f, 140f);
        if (movementInput.x == 1f && movementInput.y == 1f) dashRotation = new Vector3(0f, 0f, 185f);
        if (movementInput.x == 0f && movementInput.y == 1f) dashRotation = new Vector3(0f, 0f, 230f);
        if (movementInput.x == -1f && movementInput.y == 1f) dashRotation = new Vector3(0f, 0f, 275f);
        if (movementInput.x == -1f && movementInput.y == 0f) dashRotation = new Vector3(0f, 0f, 320f);
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
                        StartCoroutine(SpawnAttackCircle(1));
                        animator.SetFloat("AttackDirection", 1f);
                        break;
                    case 2:
                        StartCoroutine(SpawnAttackCircle(2));
                        animator.SetFloat("AttackDirection", 2f);
                        break;
                    case 3:
                        StartCoroutine(SpawnAttackCircle(3));
                        animator.SetFloat("AttackDirection", 3f);
                        break;
                    case 4:
                        StartCoroutine(SpawnAttackCircle(4));
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

                StartCoroutine(SpawnAttackCircle(1));

            }

            if (!attacked && Input.GetKeyDown(KeyCode.LeftArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 2f);

                StartCoroutine(SpawnAttackCircle(2));
              
            }

            if (!attacked && Input.GetKeyDown(KeyCode.UpArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
                
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 3f);

                StartCoroutine(SpawnAttackCircle(3));

            }

            if (!attacked && Input.GetKeyDown(KeyCode.RightArrow)) {
                attacked = true;
                timeBtwAttack = startTimeBtwAttack;
               
                animator.SetTrigger("Attack");
                animator.SetFloat("AttackDirection", 4f);


                StartCoroutine(SpawnAttackCircle(4));
                
            }
            
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    IEnumerator IncreaseSpeed() {
        speed += dashSpeed;
        yield return new WaitForSeconds(0.225f);
        speed -= dashSpeed;
    }

    IEnumerator SpawnAttackCircle(int pos)
    {
        Vector2 attackPosition = Vector2.zero;

        yield return new WaitForSeconds(0.25f);

        if (pos == 1) attackPosition = attackDownPos.position;
        if (pos == 2) attackPosition = attackLeftPos.position;
        if (pos == 3) attackPosition = attackUpPos.position;
        if (pos == 4) attackPosition = attackRightPos.position;

        Collider2D[] enemies = null;
        enemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayerMask);

        for (int i = 0; i < enemies.Length; ++i)
        {
            enemies[i].GetComponent<Enemy>().HitByPlayer(weaponDamage, knockbackStrength, new Vector2(transform.position.x, transform.position.y));
        }
    }


    public void SetStats(){
        
        weaponDamage = weapon.damage + damageBoost + 5*PlayerPrefs.GetInt("DamageLevel",0);
        maxHealth = baseHealth + 10*PlayerPrefs.GetInt("HealthLevel",0);
        health = maxHealth;
        speed = baseSpeed + 0.5f*PlayerPrefs.GetInt("SpeedLevel",0);
    }

    private void DestroyOtherPlayerObjects() {
        if(playerInstance == null){
            playerInstance = this;
            DontDestroyOnLoad(playerInstance);
        }
        else{
            Destroy(gameObject);
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
            if (health < 0) health = 0;
            isInvincible = true;
            iFrameTime = iFrames;

            //GetComponentInChildren<PlayerDamage>().DisableCollider();

            Color color = rend.material.color;
            color.a = 0.5f;
            rend.material.color = color;

            if(health == 0){
                Die();
            }
            else {
                if (damage > 0) {
                    Instantiate(hitAnimation, transform.position + Vector3.up, Quaternion.identity, gameObject.transform);
                    Instantiate(hitSound, transform.position, Quaternion.identity);
                }
            }
        }
        
           
    }

    
    private void Die() {
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        GameManager.Death();
        Destroy(gameObject);
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
