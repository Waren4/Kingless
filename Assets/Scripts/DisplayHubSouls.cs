using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayHubSouls : MonoBehaviour
{
    private TextMeshProUGUI soulsText;

    void Start()
    {
        soulsText = GetComponent<TextMeshProUGUI>();
        
    }

    private void Update()
    {
        soulsText.text = PlayerPrefs.GetInt("Souls", 0).ToString();
    }
}
