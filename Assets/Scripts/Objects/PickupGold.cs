using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGold : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            GameManager.gold++;
            Destroy(this.gameObject);
        }
    }
}
