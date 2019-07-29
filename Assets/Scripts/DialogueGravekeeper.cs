using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGravekeeper : MonoBehaviour
{
    [Header("Data")]
    public DialogueData dd1;
    public DialogueData dd2;
    public DialogueData dd3;
    public DialogueData dd4;
    public DialogueData dd5;

    private DialogueStarter dialogueStarterScript;
    private DialogueData dialogueData;

    private int deaths;
    private int level;

    void Start()
    {
        dialogueStarterScript = GetComponent<DialogueStarter>();
        dialogueStarterScript.dd = dd1;

        deaths = PlayerPrefs.GetInt("Deaths", 0);
        level = PlayerPrefs.GetInt("Level", 1);

        if (deaths >= 1) {
            dialogueStarterScript.dd = dd2;
        }
        if (deaths >= 2) {
            dialogueStarterScript.dd = dd3;
        }
        if (level >= 3) {
            dialogueStarterScript.dd = dd4;
        }
        if(level >= 5) {
            dialogueStarterScript.dd = dd5;
        }
    }
}
