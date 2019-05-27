using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    [Header("Stats")]
    public float normalSpeed;
    public float attackingSpeed;
    public float changeDirectionTime;
    public int collisionDamage;
    public float speed;

    private float moveTimer;

    public Transform visionTransform;

    private Animator animator;
    private Vector3 direction;
    private Rigidbody2D rb;
    private Queue<Vector3> q;
    
    void Start() {
        direction = new Vector3(0f, -1f);
        moveTimer = changeDirectionTime;
        speed = normalSpeed;

        q = new Queue<Vector3>();
        q.Enqueue(direction);
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    
    void FixedUpdate() {
        if(moveTimer > 0f) {
            Move();
        }
        else {
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerCollider"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);
            speed = normalSpeed;

        }
    }

    /*
    private void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Wall")){

            ChangeDirection();
        }
    }

    */

    private void Move() {
        if(speed == normalSpeed) TickMoveTimer();

        rb.AddForce(direction * speed);
    }


    private void ChangeDirection() {

        Vector3 dir = direction;

        if(q.Count == 4){
            q.Clear();
        }

        while (q.Contains(dir)) {
            GetRandomDirection();
            dir = direction;
        }

        q.Enqueue(direction);
        moveTimer = changeDirectionTime;
    }

    private void OppositeDirection() {
        if(direction.y == -1) {
            direction.y = 1f;
            visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            animator.SetFloat("MoveDirection", 3f);
        }
        else {
            if (direction.y == 1) {
                direction.y = -1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                animator.SetFloat("MoveDirection", 1f);
            }
            else {
                if(direction.x == -1) {
                    direction.x = 1f;
                    visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                    animator.SetFloat("MoveDirection", 4f);
                }
                else{
                    if(direction.x == 1) {
                        direction.x = -1f;
                        visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                        animator.SetFloat("MoveDirection", 2f);
                    }
                }
            }
        }
        moveTimer = changeDirectionTime;
    }

    private void GetRandomDirection() {
        int ranDirection = Random.Range(1, 5);

        direction = new Vector3(0f, 0f);
        switch (ranDirection)
        {
            case 1:
                direction.y = -1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f,0f,180f));
                animator.SetFloat("MoveDirection", 1f);
                break;
            case 2:
                direction.x = -1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                animator.SetFloat("MoveDirection", 2f);
                break;
            case 3:
                direction.y = 1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                animator.SetFloat("MoveDirection", 3f);
                break;
            case 4:
                direction.x = 1f;
                visionTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                animator.SetFloat("MoveDirection", 4f);
                break;
            default:
                break;
        }

    }


    private void TickMoveTimer(){
        moveTimer -= Time.deltaTime;
    }
}
