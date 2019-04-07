using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float flashTime;

    private SpriteRenderer sRenderer;
    private Color originalColor;

    private void Start() {
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        flashTime = 0.2f;
    }

    public void TakeDamage(int damageTaken){
        health -= damageTaken;
        if(health <= 0) {
            Die();
        }
        FlashOnHit();
    }

    private void FlashOnHit() {
        sRenderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor() {
        sRenderer.color = originalColor;
    }

    private void Die() {
        Destroy(gameObject);
    }
}
