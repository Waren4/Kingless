using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int score = 0;

    public static int difficulty = 2;

    public void ExitToHub() {
        SceneManager.LoadScene(1);
    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);
    }


    public void StartDungeon() {
        FloorGenerator.floorNumber = 1;
        SceneManager.LoadScene(2);
    }

    public static void NextFloor(){
        FloorGenerator.floorNumber += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Death(){
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DropDeath>().Drop();
    }

    



    public void SetDifficulty(int dif) {
        if(dif == 1)
        {
            difficulty = 1;
            PlayerController.maxHealth = 200;
            PlayerController.damageBoost = 10;
        }
        if(dif == 2)
        {
            difficulty = 2;
            PlayerController.maxHealth = 100;
            PlayerController.damageBoost = 0;
        }
        if(dif == 3)
        {
            difficulty = 3;
            PlayerController.maxHealth = 75;
            PlayerController.damageBoost = 0;
            
        }
    }

    public void QuitApp() {
        Application.Quit();
    }
}
