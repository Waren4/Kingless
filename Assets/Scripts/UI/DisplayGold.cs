using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGold : MonoBehaviour
{
    private TextMeshProUGUI goldText;

    void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        SetGold();
    }

    private void SetGold()
    {
        goldText.text = GameManager.gold.ToString();
    }
}
