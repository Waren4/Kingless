using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMatthew : MonoBehaviour
{
    [Header("Dialogue Script")]
    public Dialogue dialogueScript;

    [Header("Data")]
    public DialogueData dd1;
    public DialogueData dd2;
    public DialogueData dd3;
    public DialogueData dd4;
    public DialogueData dd5;

    [Header("Shop")]
    public GameObject shop;

    private Shop shopScript;
    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;


    // keys: 1 - dash boots;  2 -  map

    [Header("Icons")]
    public Sprite[] icons;

    private int[] costs = { 0, 25, 50 };
    private string[] descriptions = { "", "Unlocks Dash Ability", "Unlocks Dungeon Map" };
    

    private int deaths;
    private int level;
    private int sellKey;

    private bool shopIsActive;
    

    void Start()
    {
        dialogueStarterScript = GetComponent<DialogueStarter>();
        dialogueStarterScript.dd = dd1;

        shopScript = shop.GetComponent<Shop>();

        shopIsActive = false;

        deaths = PlayerPrefs.GetInt("Deaths", 0);

        sellKey = 0;

        if (deaths >= 1 && (PlayerPrefs.GetInt("HasDash",0) == 0)) {
            dialogueStarterScript.dd = dd2;
            sellKey = 1;
        }
        if (deaths >= 1 && (PlayerPrefs.GetInt("HasDash", 0) == 1)) {
            dialogueStarterScript.dd = dd3;
            sellKey = 0;
        }
        if (deaths >= 2 && (PlayerPrefs.GetInt("HasDash",0) == 1)) {
            dialogueStarterScript.dd = dd4;
            sellKey = 2;
        }
        if (deaths >= 2 && (PlayerPrefs.GetInt("HasMap",0) == 1)) {
            dialogueStarterScript.dd = dd5;
        }
        Debug.Log(sellKey);

    }
 

    private void OnTriggerStay2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            if(sellKey != 0 && Input.GetKeyDown(KeyCode.E)) {
                Debug.Log(4);
                shopScript.itemCost = costs[sellKey];
                shopScript.costText.text = costs[sellKey].ToString();
                shopScript.itemIcon.sprite = icons[sellKey];
                shopScript.itemDescription.text = descriptions[sellKey];
                shopScript.itemKey = sellKey;
                
                shop.SetActive(true);
                shopIsActive = true;        
            }
            if (shopIsActive && !dialogueScript.inDialogue) {shop.SetActive(false); shopIsActive = false;  Debug.Log(5); }
        }
    }

    public void ResetSellKey() {
        sellKey = 0;
    }
}
