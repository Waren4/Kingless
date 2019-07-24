using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayExperience : MonoBehaviour
{
    private TextMeshProUGUI expText;

    void Start()
    {
        expText = GetComponent<TextMeshProUGUI>();
        GetExperience();
    }

    void Update() {
        GetExperience();   
    }

    public void GetExperience()
    {
        expText.text = "Experience " + '\n' + PlayerPrefs.GetInt("Experience",0).ToString();
    }

    
}

