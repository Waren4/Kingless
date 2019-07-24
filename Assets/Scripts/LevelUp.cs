using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUp : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Button attackButton;
    public Button healthButton;
    public Button speedButton;

    private int currentExp;
    private int expNeeded;

    private void Update() {
        currentExp = PlayerPrefs.GetInt("Experience",0);
        expNeeded = PlayerPrefs.GetInt("ExpToLevel", 400);

        if(currentExp < expNeeded){
            attackButton.interactable = false;
            healthButton.interactable = false;
            speedButton.interactable = false;
        }
        else
        {
            attackButton.interactable = true;
            healthButton.interactable = true;
            speedButton.interactable = true;
        }

        text.text = expNeeded.ToString() + '\n' + "exp needed";
    }

    public void Level()
    {
        int lvl = PlayerPrefs.GetInt("Level", 1);
        lvl++;
        PlayerPrefs.SetInt("Level", lvl);

        int exp = PlayerPrefs.GetInt("Experience", 0);
        exp -= expNeeded;
        PlayerPrefs.SetInt("Experience", exp);

        int newExpNeeded = expNeeded;
        newExpNeeded = (int) (newExpNeeded * 1.25f);
        PlayerPrefs.SetInt("ExpToLevel", newExpNeeded);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetStats();
    }

    public void LevelAttack() {
        GiveAttackBonus();
        Level();

    }

    public void LevelHealth() { 
        GiveHealthBonus();
        Level();

    }

    public void LevelSpeed() {
        GiveSpeedBonus();
        Level();

    }

    public void GiveAttackBonus()
    {
        int level = PlayerPrefs.GetInt("AttackLevel", 0);
        level += 1;
        PlayerPrefs.SetInt("AttackLevel", level);
    }

    public void GiveHealthBonus()
    {
        int level = PlayerPrefs.GetInt("HealthLevel", 0);
        level += 1;
        PlayerPrefs.SetInt("HealthLevel", level);
    }

    public void GiveSpeedBonus()
    {
        int level = PlayerPrefs.GetInt("SpeedLevel", 0);
        level += 1;
        PlayerPrefs.SetInt("SpeedLevel", level);
    }

}
