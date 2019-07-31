using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHighscores : MonoBehaviour
{
    public TextMeshProUGUI highscore1, highscore2, highscore3, highscore4, highscore5, highscore6, highscore7, highscore8, highscore9, highscore10;

    private void OnEnable(){
        highscore1.SetText(PlayerPrefs.GetInt("Highscore1", 0).ToString());
        highscore2.SetText(PlayerPrefs.GetInt("Highscore2", 0).ToString());
        highscore3.SetText(PlayerPrefs.GetInt("Highscore3", 0).ToString());
        highscore4.SetText(PlayerPrefs.GetInt("Highscore4", 0).ToString());
        highscore5.SetText(PlayerPrefs.GetInt("Highscore5", 0).ToString());
        highscore6.SetText(PlayerPrefs.GetInt("Highscore6", 0).ToString());
        highscore7.SetText(PlayerPrefs.GetInt("Highscore7", 0).ToString());
        highscore8.SetText(PlayerPrefs.GetInt("Highscore8", 0).ToString());
        highscore9.SetText(PlayerPrefs.GetInt("Highscore9", 0).ToString());
        highscore10.SetText(PlayerPrefs.GetInt("Highscore10", 0).ToString());
    }
}
