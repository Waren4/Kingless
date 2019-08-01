using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{

    //keys: 1 - Dash Boots; 2 - Map

    public Image itemIcon;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI itemDescription;

    public Button buyButton;

    public int itemCost;
    public int itemKey;

    private int playerGold;


    private PlayerController playerScript;

    private void Start() {
        playerGold = PlayerPrefs.GetInt("Gold", 0);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    private void Update() {
        if (playerGold >= itemCost) buyButton.interactable = true;
        else buyButton.interactable = false;
    }


    public void BuyItem() {
        PayGold();
        if (itemKey == 1) {
            PlayerPrefs.SetInt("HasDash", 1);
            playerScript.SetDash();
            buyButton.interactable = false;
            this.gameObject.SetActive(false);
        }
        if (itemKey == 2) {
            PlayerPrefs.SetInt("HasMap", 1);
            buyButton.interactable = false;
            this.gameObject.SetActive(false);
        }
    }

    private void PayGold() {
        int number = PlayerPrefs.GetInt("Gold", 0);
        number -= itemCost;
        PlayerPrefs.SetInt("Gold", number);

    }
}
