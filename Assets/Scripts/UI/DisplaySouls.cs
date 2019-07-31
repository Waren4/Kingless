using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySouls : MonoBehaviour
{
    private TextMeshProUGUI soulsText;

    void Start()
    {
        soulsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        SetSouls();
    }

    private void SetSouls()
    {
        soulsText.text = GameManager.souls.ToString();
    }
}
