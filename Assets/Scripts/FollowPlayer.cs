using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    Vector3 offset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    
    void Update() {
        Follow();
    }

    public void GetPlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
    private void Follow() {
        transform.position = player.transform.position + offset;
    }
}
