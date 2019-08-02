using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private GameObject enemy;
    private Collider2D winCollider;

    private void Start() {
        winCollider = GetComponent<Collider2D>();        
    }

    private void Update() {

        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if(enemy == null) {
            winCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            GameManager.Win();
        }
    }
}
