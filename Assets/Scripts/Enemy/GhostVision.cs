using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVision : MonoBehaviour
{

    private float slowSpeed;
    private float fastSpeed;

    private Animator animator;
    private Ghost ghostScript;

    void Start(){

        ghostScript = GetComponentInParent<Ghost>();
        animator = GetComponentInParent<Animator>();

        slowSpeed = ghostScript.normalSpeed;
        fastSpeed = ghostScript.attackingSpeed;
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            animator.SetBool("IsAttacking", true);
            ghostScript.speed = fastSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("PlayerCollider")) {
            animator.SetBool("IsAttacking", false);
            ghostScript.speed = slowSpeed;
        }
    }
}
