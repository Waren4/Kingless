using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{

    public float timer;

    private Collider2D col;

   
    void Start() {
        col = GetComponent<Collider2D>();
    }

    
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0f) col.enabled = true;

    }
}
