using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
   
    private BoxCollider2D col;

    private float iTime;


    private void Start() {
        col = GetComponent<BoxCollider2D>();
        iTime = GetComponentInParent<PlayerController>().iFrames;
    }

    public void DisableCollider() {
        col.enabled = false;
        Invoke("EnableCollider", iTime);
    }

    public void EnableCollider(){
        col.enabled = true;
    }

}
