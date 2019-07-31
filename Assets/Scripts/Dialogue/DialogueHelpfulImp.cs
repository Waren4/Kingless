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

    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;

    private int deaths;
    private int level;

    void Start() {
        dialogueStarterScript= GetComponent<DialogueStarter>();
        dialogueStarterScript.dd = dd1;

        deaths = PlayerPrefs.GetInt("Deaths", 0);

        if(deaths >= 1) {
            dialogueStarterScript.dd = dd2;
        }
        if(deaths >= 2) {
            dialogueStarterScript.dd = dd3;
        }
        if(deaths >= 5) {
            dialogueStarterScript.dd = dd4;
        }
    }

    
}
