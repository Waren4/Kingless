using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemies;
    public GameObject doorWalls;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            enemies.SetActive(true);
            doorWalls.SetActive(true);
            Destroy(gameObject);
            
        }
    }
}
