using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueData dd;
    public Dialogue dialogueScript;
    public GameObject sprite;

    private bool inDialogue;

    private void Update() {
        inDialogue = dialogueScript.inDialogue;
    }

    private void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            
            if (!inDialogue) {
                sprite.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
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
}
