using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy
{
    public int collisionDamage;
    public float speed;
    public float startTimeBtwMoves;
    public float moveDistance;

    private Vector2 moveDestination;
    private float timeBetweenMoves;

    void Start() {
        timeBetweenMoves = 0f; 
    }

    
    void Update() {
        if (timeBetweenMoves <= 0f)
        {
            GetRandomDirection();
            StartCoroutine(Move(moveDestination));
            timeBetweenMoves = startTimeBtwMoves;
        }
        else
        {
            timeBetweenMoves -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerController>().TakeDamage(collisionDamage);
            other.GetComponent<PlayerDamage>().DisableCollider();
        }
    }

    private void GetRandomDirection()
    {
        int ranDirection = Random.Range(1, 5);
        moveDestination = new Vector2(transform.position.x, transform.position.y);

        switch (ranDirection)
        {
            case 1:
                moveDestination.y -= moveDistance;
                break;
            case 2:
                moveDestination.x -= moveDistance;
                break;
            case 3:
                moveDestination.y += moveDistance;
                break;
            case 4:
                moveDestination.x += moveDistance;
                break;
            default:
                break;
        }
    }

    IEnumerator Move(Vector2 destination)
    {
        while (new Vector2(transform.position.x, transform.position.y) != destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}
