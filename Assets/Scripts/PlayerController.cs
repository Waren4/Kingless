using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed;

    private Rigidbody2D rb;
    private Vector2 movementInput,movement;
    private Animator animator;
    public float magnitude;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        movementInput.Normalize();
        movement = movementInput * speed;
        if (movement.magnitude > 0) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Magnitude", movementInput.magnitude);
        ;
    }

    private void FixedUpdate() {
        Move();
    }


    void Move() {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

}
