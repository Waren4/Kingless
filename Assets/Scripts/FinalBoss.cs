using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [Header("Phase Change Animation")]
    public GameObject phaseChangeAnimation;

    [Header("Projectiles")]
    public GameObject fireball;
    public GameObject bullet;

    [Header("Stats")]
    public int collisionDamage;
    public int speed;
    public float fireballSpeed;
    public float bulletSpeed;
    public float timeBetweenAttacks;

    [Header ("Positions")]
    public Transform roomCenter;
    public Transform leftCenter;
    public Transform rightCenter;
    public Transform downWall;
    public Transform upWall;
    public Transform leftWall;
    public Transform rightWall;

    public Transform attackRight;
    public Transform attackLeft;
    public Transform attackDown;
    public Transform attackUp;
    public Transform attackDownLeft;
    public Transform attackLeftUp;
    public Transform attackUpRight;
    public Transform attackRightDown;

    private Rigidbody2D rb;
    private Animator animator;
    private GameObject player;
    private Enemy enemyScript;

    private float positionOffset = 0.18f;
    private float attackTimer;
    private int health;
    private bool transformed = false;

    void Start() {

        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyScript = GetComponent<Enemy>();


        attackTimer = 2f;

    }

    void Update() {
        animator.SetFloat("Horizontal", transform.position.x - roomCenter.position.x);

        health = enemyScript.health;

        if(health <= 100 && !transformed) {
            EnterPhase2();
        }

        if (attackTimer <= 0f) {

            if (!transformed)
            {
                int i = Random.Range(0, 3);
                if (i == 0)
                {
                    StartCoroutine(PhaseOneCenterAttack());
                    attackTimer = 5f;
                }
                if (i == 1)
                {
                    StartCoroutine(PhaseOneWallsAttack());
                    attackTimer = 14f;
                }
                if (i == 2)
                {
                    StartCoroutine(PhaseOneArcAtatck());
                    attackTimer = 13f;
                }
            }
            else {
                int i = Random.Range(0, 3);
                if (i == 0) {
                    StartCoroutine(PhaseTwoCenterAttack());
                    attackTimer = 7f;
                }
                if (i == 1) {
                    StartCoroutine(PhaseTwoWallsAttack());
                    attackTimer = 14f;
                }
                if (i == 2) {
                    StartCoroutine(PhaseTwoArcAtatck());
                    attackTimer = 13f;
                }
            }
        }
        else attackTimer -= Time.deltaTime;
    
        
    }

    // 1 down 2 left 3 up 4 right 5 downleft 6 leftup 7 upright 8 rightdown
    void ShootFireball(Transform attackDirection, int direction) {

        GameObject projectile;
        Transform tf;
        Rigidbody2D rb;

        projectile = Instantiate(fireball, attackDirection.position, Quaternion.identity) as GameObject;

        rb = projectile.GetComponent<Rigidbody2D>();
        tf = projectile.GetComponent<Transform>();

        if(direction == 1) {
            rb.AddForce(Vector3.down * fireballSpeed);
        }
        if(direction == 2) {
            rb.AddForce(Vector3.left * fireballSpeed);
            tf.Rotate(0, 0, 270);
        }
        if (direction == 3) {
            rb.AddForce(Vector3.up * fireballSpeed);
            tf.Rotate(0, 0, 180);
        }
        if (direction == 4) {
            rb.AddForce(Vector3.right * fireballSpeed);
            tf.Rotate(0, 0, 90);
        }
        if(direction == 5) {
            rb.AddForce((Vector3.down + Vector3.left).normalized * fireballSpeed);
            tf.Rotate(0, 0, 315);
        }
        if (direction == 6) {
            rb.AddForce((Vector3.left + Vector3.up).normalized * fireballSpeed);
            tf.Rotate(0, 0, 225);
        }
        if (direction == 7) {
            rb.AddForce((Vector3.up + Vector3.right).normalized * fireballSpeed);
            tf.Rotate(0, 0, 135);
        }
        if (direction == 8) {
            rb.AddForce((Vector3.right + Vector3.down).normalized * fireballSpeed);
            tf.Rotate(0, 0, 45);
        }
    }

    void ShootBullets() {
        Vector3[] directions = { Vector3.up, (Vector3.up + Vector3.left).normalized, Vector3.left, (Vector3.left + Vector3.down).normalized,
                                 Vector3.down, (Vector3.down + Vector3.right).normalized, Vector3.right, (Vector3.right + Vector3.up).normalized
                                };
        GameObject projectile;
        Rigidbody2D rb;

        int i;

        for(i = 0; i < 8; ++i) {
            projectile = Instantiate(bullet, transform.position + directions[i], Quaternion.identity) as GameObject;

            rb = projectile.GetComponent<Rigidbody2D>();

            rb.AddForce(directions[i] * bulletSpeed);

        }
        
        
    }

    void ShootBulletsArc(Vector3 direction) {
        Vector3[] directions = new Vector3[7];

        if (direction == Vector3.left) {
            directions[0] = (direction + new Vector3(0, 0.75f)).normalized;
            directions[1] = (direction + new Vector3(0, 0.5f)).normalized;
            directions[2] = (direction + new Vector3(0, 0.25f)).normalized;
            directions[3] = (direction + new Vector3(0, 0f)).normalized;
            directions[4] = (direction + new Vector3(0, -0.25f)).normalized;
            directions[5] = (direction + new Vector3(0, -0.5f)).normalized;
            directions[6] = (direction + new Vector3(0, -0.75f)).normalized;
        }
        if (direction == Vector3.right) {
            directions[0] = (direction + new Vector3(0, 0.75f)).normalized;
            directions[1] = (direction + new Vector3(0, 0.5f)).normalized;
            directions[2] = (direction + new Vector3(0, 0.25f)).normalized;
            directions[3] = (direction + new Vector3(0, 0f)).normalized;
            directions[4] = (direction + new Vector3(0, -0.25f)).normalized;
            directions[5] = (direction + new Vector3(0, -0.5f)).normalized;
            directions[6] = (direction + new Vector3(0, -0.75f)).normalized;
        }
        if (direction == Vector3.up) {
            directions[0] = (direction + new Vector3(0.75f,0)).normalized;
            directions[1] = (direction + new Vector3(0.5f,0)).normalized;
            directions[2] = (direction + new Vector3(0.25f,0)).normalized;
            directions[3] = (direction + new Vector3(0f,0)).normalized;
            directions[4] = (direction + new Vector3(-0.25f,0)).normalized;
            directions[5] = (direction + new Vector3(-0.5f,0)).normalized;
            directions[6] = (direction + new Vector3(-0.75f,0)).normalized;
        }
        if (direction == Vector3.down) {
            directions[0] = (direction + new Vector3(0.75f, 0)).normalized;
            directions[1] = (direction + new Vector3(0.5f, 0)).normalized;
            directions[2] = (direction + new Vector3(0.25f, 0)).normalized;
            directions[3] = (direction + new Vector3(0f, 0)).normalized;
            directions[4] = (direction + new Vector3(-0.25f, 0)).normalized;
            directions[5] = (direction + new Vector3(-0.5f, 0)).normalized;
            directions[6] = (direction + new Vector3(-0.75f, 0)).normalized;
        }




        GameObject projectile;
        Rigidbody2D rb;

        int i;

        for (i = 0; i < 7; ++i) {
            projectile = Instantiate(bullet, transform.position + directions[i], Quaternion.identity) as GameObject;

            rb = projectile.GetComponent<Rigidbody2D>();

            rb.AddForce(directions[i] * bulletSpeed);

        }
    }


    IEnumerator PhaseOneArcAtatck(){
        Transform location;
        Vector3 shootDirection = Vector3.zero;

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1.8f);
    }


    IEnumerator PhaseOneWallsAttack(){
        
        StartCoroutine(MoveTo(leftWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackRight, 4);

        ShootFireball(attackUp, 3);

        ShootFireball(attackDown, 1);

        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(downWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackUp, 3);

        ShootFireball(attackRight, 4);

        ShootFireball(attackLeft, 2);

        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(rightWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackLeft, 2);

        ShootFireball(attackUp, 3);

        ShootFireball(attackDown, 1);


        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackDown, 1);

        ShootFireball(attackRight, 4);

        ShootFireball(attackLeft, 2);

        yield return new WaitForSeconds(0.85f);
    }


    IEnumerator PhaseOneCenterAttack() {

        StartCoroutine(MoveTo(RandomCenterLocation()));

        yield return new WaitForSeconds(1f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1f);
    }


    IEnumerator PhaseTwoArcAtatck()
    {
        Transform location;
        Vector3 shootDirection = Vector3.zero;

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        location = RandomWallLocation();

        if (location == downWall) shootDirection = Vector3.up;
        if (location == upWall) shootDirection = Vector3.down;
        if (location == leftWall) shootDirection = Vector3.right;
        if (location == rightWall) shootDirection = Vector3.left;

        StartCoroutine(MoveTo(location));

        yield return new WaitForSeconds(1.8f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.55f);

        ShootBulletsArc(shootDirection);

        yield return new WaitForSeconds(0.65f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1.8f);
    }

    IEnumerator PhaseTwoCenterAttack()
    {

        StartCoroutine(MoveTo(RandomCenterLocation()));

        yield return new WaitForSeconds(1f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        ShootBullets();

        yield return new WaitForSeconds(0.55f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1f);
    }


    IEnumerator PhaseTwoWallsAttack()
    {

        StartCoroutine(MoveTo(leftWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackRight, 4);

        ShootFireball(attackUp, 3);

        ShootFireball(attackDown, 1);

        ShootFireball(attackRightDown, 8);

        ShootFireball(attackUpRight, 7);

        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(downWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackUp, 3);

        ShootFireball(attackRight, 4);

        ShootFireball(attackLeft, 2);

        ShootFireball(attackUpRight, 7);

        ShootFireball(attackLeftUp, 6);

        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(rightWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackLeft, 2);

        ShootFireball(attackUp, 3);

        ShootFireball(attackDown, 1);

        ShootFireball(attackLeftUp, 6);

        ShootFireball(attackDownLeft, 5);

        yield return new WaitForSeconds(0.85f);

        StartCoroutine(MoveTo(upWall));

        yield return new WaitForSeconds(1.8f);

        ShootFireball(attackDown, 1);

        ShootFireball(attackRight, 4);

        ShootFireball(attackLeft, 2);

        ShootFireball(attackDownLeft, 5);

        ShootFireball(attackRightDown, 8);

        yield return new WaitForSeconds(0.85f);
    }


    IEnumerator MoveTo(Transform location) {

        Vector3 direction;
        float distance; 

        distance = (location.position - transform.position).magnitude;
        direction = (location.position - transform.position).normalized;


        while (distance > positionOffset) {
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

            distance = (location.position - transform.position).magnitude;
            direction = (location.position - transform.position).normalized;

            yield return null;

        }
    }

    private void EnterPhase2(){
        transformed = true;
        Instantiate(phaseChangeAnimation, transform.position, Quaternion.identity);
        Invoke("Phase2Trigger", 0.3f);
    }

    private void Phase2Trigger(){
        animator.SetTrigger("Phase 2");
    }

    private Transform RandomWallLocation() {
        int i = Random.Range(0, 4);
        if (i == 0) return upWall;
        if (i == 1) return leftWall;
        if (i == 2) return downWall;
        if (i == 3) return rightWall;
        return null;
    }

    private Transform RandomCenterLocation() {
        int i = Random.Range(0, 3);
        if (i == 0) return roomCenter;
        if (i == 1) return leftCenter;
        if (i == 2) return rightCenter;
        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider")) {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);

        }
    }


}
