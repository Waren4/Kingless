using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHubGold : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
        
    }

    private void Update()
    {
        goldText.text = PlayerPrefs.GetInt("Gold", 0).ToString();

    }
}
