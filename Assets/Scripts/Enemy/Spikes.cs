using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    private int collisionDamage = 10;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerController>().TakeDamage(collisionDamage);
        }
    }
}
