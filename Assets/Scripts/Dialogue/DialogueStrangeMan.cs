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
    public DialogueData dd6;
    public DialogueData dd7;
    public DialogueData dd8;


    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;

    private GameObject player;

    private int soulLevel;
    private int soulsNumber;
    private bool paid = false;
    private bool ePress;
    private float range = 2.3f;
    private float distanceToPlayer;
    

    private int[] soulsToPay = { 0, 1, 2, 3, 4, 5, 10, 10000, 10, 10, 12 };

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

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

    private void Update(){
        ePress = Input.GetKeyDown(KeyCode.E);

        GetDistanceToPlayer();

        if (PlayerPrefs.GetInt("SMover", 0) == 0)
        {
            if (distanceToPlayer <= range)
            {
                if (soulsNumber >= soulsToPay[soulLevel + 1])
                {
                    if (!paid && ePress)
                    {
                        PaySouls();
                        IncreaseSoulLevel();
                        SetData();
                        paid = true;
                    }
                }

            }
            else
            {
                paid = false;
            }
        }
        else if (ePress) dialogueStarterScript.dd = dd8;
    }

    /*
    private void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            if(soulsNumber >= soulsToPay[soulLevel + 1]) {
                if(!paid && ePress) {
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
    */

    private void GetDistanceToPlayer() {
        distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
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
        if (soulLevel == 3) dialogueStarterScript.dd = dd4;
        if (soulLevel == 4) dialogueStarterScript.dd = dd5;
        if (soulLevel == 5) dialogueStarterScript.dd = dd6;
        if (soulLevel == 6) {
            dialogueStarterScript.dd = dd7;
            PlayerPrefs.SetInt("SMover", 1);
        }
    }
}
