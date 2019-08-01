using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [Header ("Direction")]
    public int exitDirection;

    private GameObject cam;
    private GameObject playerOnMap;

    private void Start() {

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        playerOnMap = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>().playerIcon;

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            switch (exitDirection)
            {
                
                case 1:
                    cam.transform.position -= new Vector3(0f,17f,0f);
                    other.transform.position += new Vector3(0f, -3f, 0f);
                    playerOnMap.transform.position += new Vector3(0f, -0.5625f, 0f);
                    break;
                case 2:
                    cam.transform.position -= new Vector3(30f,0f,0f);
                    other.transform.position += new Vector3(-2f, 0f, 0f);
                    playerOnMap.transform.position += new Vector3(-1f, 0f, 0f);
                    break;
                case 3:
                    cam.transform.position += new Vector3(0f,17f,0f);
                    other.transform.position += new Vector3(0f, 2f, 0f);
                    playerOnMap.transform.position += new Vector3(0f, 0.5625f, 0f);
                    break;
                case 4:
                    cam.transform.position += new Vector3(30f,0f,0f);
                    other.transform.position += new Vector3(2, 0f, 0f);
                    playerOnMap.transform.position += new Vector3(1f, 0f, 0f);
                    break;
                default:
                    break;

            }
            other.GetComponent<PlayerController>().TakeDamage(0);
        }
    }
}
