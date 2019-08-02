using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public DialogueData dd;
    public Dialogue dialogueScript;
    public GameObject sprite;

    private bool inDialogue;
    private bool ePress;

    private void Update() {
        inDialogue = dialogueScript.inDialogue;
        ePress = Input.GetKeyDown(KeyCode.E);
    }

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
}
