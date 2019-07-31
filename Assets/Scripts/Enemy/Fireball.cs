using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header ("Stats")]
    public int damage;

    private PlayerController player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();    
    }
 
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Wall")) {
            DestroyProjectile();
        }

        if (col.CompareTag("PlayerCollider")) {
            player.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile() {
        Destroy(gameObject);
    }
}
