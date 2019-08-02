using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHelpfulImp : MonoBehaviour
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
    public DialogueData dd9;
    public DialogueData dd10;

    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;

    private int deaths;
    private int level;

    void Start() {
        dialogueStarterScript= GetComponent<DialogueStarter>();
        dialogueStarterScript.dd = dd1;

        deaths = PlayerPrefs.GetInt("Deaths", 0);
        
        if(PlayerPrefs.GetInt("HIover",0) == 1){
            int i = Random.Range(0, 3);
            if (i == 0) dialogueStarterScript.dd = dd8;
            if (i == 1) dialogueStarterScript.dd = dd9;
            if (i == 2) dialogueStarterScript.dd = dd10;
        }
        if(deaths >= 1) {
            dialogueStarterScript.dd = dd2;
        }
        if(deaths >= 2) {
            dialogueStarterScript.dd = dd3;
        }
        if(deaths >= 5) {
            dialogueStarterScript.dd = dd4;
        }
        if(PlayerPrefs.GetInt("Level", 0) >= 2 ) {
            dialogueStarterScript.dd = dd6;
        }
        if(PlayerPrefs.GetInt("Souls",0) >= 5) {
            dialogueStarterScript.dd = dd5;
        }
        if (deaths >= 100) {
            dialogueStarterScript.dd = dd7;
            PlayerPrefs.SetInt("HIover", 1);
        }
    }

    
}
