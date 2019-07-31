using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayScore : MonoBehaviour
{

    private TextMeshProUGUI scoreText;

    void Start(){
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    
    void Update(){
        SetScore();
    }

    private void SetScore() {
        scoreText.text = "Score: " + GameManager.score.ToString();
    }
}
