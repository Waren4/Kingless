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
        ResetPlayerHealth();
        UpdateHighscores(score);
        ResetScore();
    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);
        UpdateHighscores(score);
        ResetScore();
        DestroyPlayer();
    }


    public void StartDungeon() {
        FloorGenerator.floorNumber = 1;
        SceneManager.LoadScene(2);
    }

    public static void GiveScore(int addedScore) {
        score += addedScore;
    }

    public static void ResetScore(){
        score = 0;
    }

    public static void UpdateHighscores(int scoreAchieved){
        int[] highscores = { 0, PlayerPrefs.GetInt("Highscore1",0), PlayerPrefs.GetInt("Highscore2", 0), PlayerPrefs.GetInt("Highscore3", 0), PlayerPrefs.GetInt("Highscore4", 0), 
                                  PlayerPrefs.GetInt("Highscore5",0), PlayerPrefs.GetInt("Highscore6",0), PlayerPrefs.GetInt("Highscore7",0), PlayerPrefs.GetInt("Highscore8",0),
                                  PlayerPrefs.GetInt("Highscore9",0), PlayerPrefs.GetInt("Highscore10",0)
        };
        int i, j;
        string hs = "Highscore";

        for (i = 1; i < 11; ++i) {
            if( scoreAchieved > highscores[i]) {

                for (j = 10; j > i; --j) {
                    highscores[j] = highscores[j - 1];
                }
                highscores[i] = scoreAchieved;
                break;
            }
        }

        for(i = 1; i < 11; ++i) {
            string key;
            key = hs + i.ToString();

            PlayerPrefs.SetInt(key, highscores[i]);
        }
    }

    public static void NextFloor(){
        FloorGenerator.floorNumber += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Death(){
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DropDeath>().Drop();
        
    }

    public static void ResetPlayerHealth() {
        try
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health = PlayerController.maxHealth;
        }
        catch { };
    }

    public static void DestroyPlayer() {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
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
