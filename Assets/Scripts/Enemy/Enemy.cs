using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [Header ("Enemy Stats")]
    public int health;
    public int giveScore;
    public bool canKnockback = true;

    [Header("Enemy Script")]
    public MonoBehaviour enemyScript;

    [Header("Death Animation")]
    public GameObject deathAnimation;

    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    private Color originalColor;
    private DropPickups dropScript;

    private float flashTime;
    
    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        dropScript = GetComponent<DropPickups>();

        originalColor = sRenderer.color;
        flashTime = 0.1f;
    }

    public void HitByPlayer(int damageTaken, float knockback, Vector2 position) {
        TakeDamage(damageTaken);
        if(canKnockback) KnockBackOnHit(knockback, position);
        //enemyScript.StopCoroutine("Move");     
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
        Color color = sRenderer.material.color;
        color.a = 0.5f;
        sRenderer.material.color = color;
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor() {
        sRenderer.color = originalColor;
        Color color = sRenderer.material.color;
        color.a = 1f;
        sRenderer.material.color = color;
    }

    private void Die() {
        dropScript.DropItems();
        Instantiate(deathAnimation, transform.position, Quaternion.identity);
        GameManager.GiveScore(giveScore);
        Destroy(gameObject);
    }

}
