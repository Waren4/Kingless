using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkeleton : MonoBehaviour
{
    [Header("Stats")]
    public int collisionDamage;
    public float speed;
    public float moveTime;
    public bool goesVertical;
    public bool startMinus;

    private Animator animator;
    private Vector3 direction;
    private Rigidbody2D rb;
    private float timer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        direction = Vector3.zero;

        if (goesVertical) direction.y = 1f;
        else direction.x = 1f;
        if(startMinus) {
            direction.y = -direction.y;
            direction.x = -direction.x;
        }

        SetAnimatorMovementParams();

        timer = 0;
    }

 
    void FixedUpdate() {
        Move();
    }


    private void Move() {
        Vector3 oldPosition;

        if(timer < moveTime) {
            oldPosition = transform.position;
            rb.AddForce(direction * speed);
            timer += Time.deltaTime;
        }
        else {
            ChangeDirection();
        }
    }

    private void ChangeDirection() {
        timer = -moveTime;
        if (goesVertical) direction.y = -direction.y;
        else direction.x = -direction.x;
        SetAnimatorMovementParams();
    }

    private void SetAnimatorMovementParams() {
        if (direction.y == -1) animator.SetFloat("MoveDirection", 1);
        if (direction.x == -1) animator.SetFloat("MoveDirection", 2);
        if (direction.y == 1) animator.SetFloat("MoveDirection", 3);
        if (direction.x == 1) animator.SetFloat("MoveDirection", 4);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerCollider")) {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);
        }
    }
}
