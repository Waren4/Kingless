using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDungeon : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            EnterDungeon();
        }
    }


    public void EnterDungeon() {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<FadeToBlack>().Fade();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StartDungeon();
    }
}
