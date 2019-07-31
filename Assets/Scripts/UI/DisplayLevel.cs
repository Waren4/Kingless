using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevel : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    void Start()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        
    }

    void Update()
    {
        GetLevel();
    }

    public void GetLevel()
    {
        levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1).ToString();
    }


}
