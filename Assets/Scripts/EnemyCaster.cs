using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaster : MonoBehaviour
{
    // AttackDirection:     1 - down    2 - left    3 - up     4 - right    

    [Header("Attack Stats")]
    public int collisionDamage;
    public float projectileSpeed;
    public float startTimeBtwAttacks;
    public float animationDelay;
    public int attackDirection;

    [Header ("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    [Header ("Projectile")]
    public GameObject projectile;
    

    private float timeBtwAttacks;

    private Vector3 projectileDirection;
    private Quaternion attackRotation;
    private Transform attackPosition;
    private Animator animator;


    void Start() {

        animator = GetComponent<Animator>();

        SetAttackPosition();
        
        timeBtwAttacks = startTimeBtwAttacks; 
    }


    void Update() {

        if(timeBtwAttacks <= 0f ) {

            animator.SetTrigger("Attack");
            animator.SetFloat("MoveDirection", attackDirection);
            animator.SetFloat("AttackDirection", attackDirection);

            Invoke("Attack", animationDelay);

            timeBtwAttacks = startTimeBtwAttacks;
        }
        else TickAttackTimer();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);

        }
    }

    private void Attack() {

        GameObject projectileInstance;
        Rigidbody2D projectileRB;

        projectileInstance = Instantiate(projectile, attackPosition.position, attackRotation) as GameObject;
        projectileRB = projectileInstance.GetComponent<Rigidbody2D>();

        projectileRB.AddForce(projectileDirection * projectileSpeed);

    }

    private void TickAttackTimer() {
        if (timeBtwAttacks > 0f) timeBtwAttacks -= Time.deltaTime;
    }

    private void SetAttackPosition() {
        switch (attackDirection)
        {
            case 1:
                attackPosition = attackDown;
                projectileDirection = Vector3.down;
                attackRotation = Quaternion.Euler(new Vector3(0f,0f,180f));
                break;
            case 2:
                attackPosition = attackLeft;
                projectileDirection = Vector3.left;
                attackRotation = Quaternion.Euler(new Vector3(0f,0f,90f));
                break;
            case 3:
                attackPosition = attackUp;
                projectileDirection = Vector3.up;
                attackRotation = Quaternion.Euler(new Vector3(0f,0f,0f));
                break;
            case 4:
                attackPosition = attackRight;
                projectileDirection = Vector3.right;
                attackRotation = Quaternion.Euler(new Vector3(0f,0f,270f));
                break;

            default:
                break;
        }
    }
}
