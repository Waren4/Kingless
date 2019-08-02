using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueData dd;
    public Dialogue dialogueScript;
    public GameObject sprite;

    private GameObject player;

    private bool inDialogue;
    private bool ePress;
    private float range = 2.3f;
    private float distanceToPlayer;

    private void Update() {
        player = GameObject.FindGameObjectWithTag("Player");

        inDialogue = dialogueScript.inDialogue;
        ePress = Input.GetKeyDown(KeyCode.E);

        GetDistanceToPlayer();
        
    }

    private void LateUpdate()
    {
        if (distanceToPlayer <= range)
        {
            if (!inDialogue)
            {
                sprite.SetActive(true);
                if (ePress)
                {
                    dialogueScript.dialogueData = dd;
                    dialogueScript.StartDialogue();
                    sprite.SetActive(false);
                }
            }
        }
        else
        {
            sprite.SetActive(false);
        }
    }



    private void GetDistanceToPlayer() {
        distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));        
    }

    /*
    private void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            
            if (!inDialogue) {
                sprite.SetActive(true);
                if (ePress) {
                    dialogueScript.dialogueData = dd;
                    dialogueScript.StartDialogue();
                    sprite.SetActive(false);
                }
            } 

        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            sprite.SetActive(false);
        }
    }
    */
}
