using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image bar;

    private Text barText;
    private PlayerController player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        barText = GetComponentInChildren<Text>();
    }

    void Update() {
        SetHealth();
        
    }

    private void SetHealth(){
        bar.fillAmount = player.health / PlayerController.maxHealth;
        barText.text = player.health.ToString() + "/" + PlayerController.maxHealth;
    }
}
