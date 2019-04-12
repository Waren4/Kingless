using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Enemy Stats")]
    public int health;

    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    private Color originalColor;

    private float flashTime;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        flashTime = 0.1f;
        
    }

    public void HitByPlayer(int damageTaken, float knockback, Vector2 position) {
        TakeDamage(damageTaken);
        KnockBackOnHit(knockback, position);
    }

    private void TakeDamage(int damageTaken){
        health -= damageTaken;
        if(health <= 0) {
            Die();
        }
        FlashOnHit();
    }

    private void KnockBackOnHit(float knockback, Vector2 position) {
        Vector2 direction = new Vector2 (transform.position.x,transform.position.y) - position;
        rb.AddForce(direction.normalized * knockback,ForceMode2D.Impulse);
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
