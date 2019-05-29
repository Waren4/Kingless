using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{

    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = Vector3.zero;

        Destroy(gameObject);
        
    }

    
}
