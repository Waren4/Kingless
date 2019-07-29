using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoul : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerCollider"))
        {
            GameManager.souls++;
            Destroy(this.gameObject);
        }
    }
}
