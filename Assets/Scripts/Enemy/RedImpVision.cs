using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedImpVision : MonoBehaviour
{

    private float slowSpeed;
    private float fastSpeed;

    private RedImp impScript;
    private Animator animator;


    void Start() {
        impScript = GetComponentInParent<RedImp>();
        animator = GetComponentInParent<Animator>();

        slowSpeed = impScript.normalSpeed;
        fastSpeed = impScript.followSpeed;
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")){
            impScript.isFollowing = true;
            impScript.isMoving = false;
            impScript.timeBetweenMoves = impScript.startTimeBtwMoves;
            impScript.speed = fastSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            impScript.isFollowing = false;
            impScript.moveTime = 0f;
            impScript.isMoving = false;
            animator.SetBool("IsMoving", false);
            /*
            
            impScript.timeBetweenMoves = impScript.startTimeBtwMoves;
            */
            impScript.speed = slowSpeed;
            
        }
    }
}
