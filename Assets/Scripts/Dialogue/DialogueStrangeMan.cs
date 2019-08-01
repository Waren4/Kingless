using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStrangeMan : MonoBehaviour
{
    [Header("Data")]
    public DialogueData dd1;
    public DialogueData dd2;
    public DialogueData dd3;
    public DialogueData dd4;
    public DialogueData dd5;

    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;

    private int soulLevel;
    private int soulsNumber;
    private bool paid = false;

    private int[] soulsToPay = { 0, 1, 2, 3, 4, 5, 7, 10, 10, 10, 12 };
    void Start()
    {
        dialogueStarterScript = GetComponent<DialogueStarter>();
        dialogueStarterScript.dd = dd1;


        /*
        PlayerPrefs.SetInt("SoulLevel", 0);
        PlayerPrefs.SetInt("Souls", 1);
        */


        soulLevel = PlayerPrefs.GetInt("SoulLevel", 0);
        soulsNumber = PlayerPrefs.GetInt("Souls", 0);



        SetData();
    }

    private void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            if(soulsNumber >= soulsToPay[soulLevel + 1]) {
                if(!paid && Input.GetKeyDown(KeyCode.E)) {
                    PaySouls();
                    IncreaseSoulLevel();
                    SetData();
                    paid = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            paid = false;
        }
    }

    private void PaySouls() {
        int number = PlayerPrefs.GetInt("Souls", 0);
        number -= soulsToPay[soulLevel + 1];
        PlayerPrefs.SetInt("Souls", number);

    }


    private void IncreaseSoulLevel() {
        int lvl = PlayerPrefs.GetInt("SoulLevel", 0);
        lvl++;
        PlayerPrefs.SetInt("SoulLevel", lvl);
        soulLevel = lvl;
    }


    private void SetData() {
        if (soulLevel == 0) dialogueStarterScript.dd = dd1;
        if (soulLevel == 1) dialogueStarterScript.dd = dd2;
        if (soulLevel == 2) dialogueStarterScript.dd = dd3;
    }
}
