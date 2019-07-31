using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{

    public FollowPlayer followScript;

    private GameObject player;

    void Start() {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = Vector3.zero;

         followScript.GetPlayer(); } catch { };

        Destroy(gameObject);
        
    }

    
}
