using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackCaster : MonoBehaviour
{
    [Header("Attack Stats")]
    public int collisionDamage;
    public float projectileSpeed;
    public float startTimeBtwAttacks;
    public float animationDelay;

    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    [Header("Projectile")]
    public GameObject projectile;

    private float timeBtwAttacks;

    private Quaternion downRotation, leftRotation, upRotation, rightRotation;
    private Animator animator;


    void Start() {

        animator = GetComponent<Animator>();

        timeBtwAttacks = startTimeBtwAttacks;

        downRotation = Quaternion.Euler(0f, 0f, 180f);
        leftRotation = Quaternion.Euler(0f, 0f, 90f);
        upRotation = Quaternion.Euler(0f, 0f, 0f);
        rightRotation = Quaternion.Euler(0f, 0f, 270f);

    }



    void Update() {
        if (timeBtwAttacks <= 0f) {

            animator.SetTrigger("Attack");
            Invoke("Attack",animationDelay);

            timeBtwAttacks = startTimeBtwAttacks;

        }
        else TickAttackTimer();
    }


    private void Attack() {

        GameObject projectileInstance;
        Rigidbody2D projectileRB;

        // Attack Down
        projectileInstance = Instantiate(projectile, attackDown.position, downRotation) as GameObject;
        projectileRB = projectileInstance.GetComponent<Rigidbody2D>();
        projectileRB.AddForce(Vector3.down * projectileSpeed);

        // Attack Left
        projectileInstance = Instantiate(projectile, attackLeft.position, leftRotation) as GameObject;
        projectileRB = projectileInstance.GetComponent<Rigidbody2D>();
        projectileRB.AddForce(Vector3.left * projectileSpeed);

        // Attack Up
        projectileInstance = Instantiate(projectile, attackUp.position, upRotation) as GameObject;
        projectileRB = projectileInstance.GetComponent<Rigidbody2D>();
        projectileRB.AddForce(Vector3.up * projectileSpeed);

        // Attack Right
        projectileInstance = Instantiate(projectile, attackRight.position, rightRotation) as GameObject;
        projectileRB = projectileInstance.GetComponent<Rigidbody2D>();
        projectileRB.AddForce(Vector3.right * projectileSpeed);


    }

    private void TickAttackTimer() {
        if (timeBtwAttacks > 0f) timeBtwAttacks -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);

        }
    }

}
