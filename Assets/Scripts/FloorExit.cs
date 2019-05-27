using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorExit : MonoBehaviour
{
    private PlayerController playerScript;

    private void OnTriggerEnter2D(Collider2D col) {
       if(col.CompareTag("Player")){
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<FadeToBlack>().Fade();
            playerScript = col.GetComponent<PlayerController>();
            playerScript.enabled = false;
            StartCoroutine(WaitForNextFloor());
        }
    }

    IEnumerator WaitForNextFloor(){
        yield return new WaitForSeconds(1.2f);
        playerScript.enabled = true;
        GameManager.NextFloor();
    }
}
