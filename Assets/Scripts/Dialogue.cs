﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialogueData;

    public GameObject dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;
    public GameObject leftButton;
    public GameObject rightButton;

    

    public bool inDialogue;

    private float wait;
    private int currentPhrase;
    private int number;
    private string character;
    private string[] phrases = new string[20];

    private PlayerController playerScript;

    private void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() {

        if (currentPhrase == 0) leftButton.SetActive(false); else leftButton.SetActive(true);
        if (currentPhrase + 1 == number) rightButton.SetActive(false); else rightButton.SetActive(true);

        if (inDialogue) {
            if (wait <= 0f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(2);
                    EndDialogue();
                }
            }
            else wait -= Time.deltaTime;
        }
    }

    public void StartDialogue() {

        Debug.Log(1);

        wait = 1f;

        playerScript.enabled = false;
        dialogueBox.SetActive(true);
        inDialogue = true;

        character = dialogueData.characterName;
        number = dialogueData.numberOfPhrases;
        for (int i = 0; i < number; ++i) phrases[i] = dialogueData.phrases[i];

        currentPhrase = 0;
        speechText.text = phrases[currentPhrase];
        nameText.text = character;
    }

    public void EndDialogue() {
        playerScript.enabled = true;
        dialogueBox.SetActive(false);
        inDialogue = false;
    }

    public void NextPhrase() {
        ++currentPhrase;
        speechText.text = phrases[currentPhrase];
    }

    public void PreviousPhrase() {
        --currentPhrase;
        speechText.text = phrases[currentPhrase];
    }

}
