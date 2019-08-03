using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static int gold = 0;
    public static int souls = 0;

    public static int mode; 

    public static int difficulty = 2;

    public void ExitToHub() {
        SceneManager.LoadScene(1);
        CountDeath();
        ResetPlayerHealth();
        UpdateHighscores(score);
        GiveExperience(score / 2);
        GiveGold(gold);
        GiveSouls(souls);
        ResetScore();
    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);

        /*
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene > 1) { 
        CountDeath();
        UpdateHighscores(score);
        GiveExperience(score / 2);
        
        } */
        ResetScore();
        DestroyPlayer();

    }

    public void StartStory() {
        SetMode(1);
        SceneManager.LoadScene(1);
    }

    public void StartEndless() {
        SetMode(2);
        FloorGenerator.floorNumber = 1;
        SceneManager.LoadScene(6);
    }

    public void RestartEndless() {
        FloorGenerator.floorNumber = 1;
        SceneManager.LoadScene(6);
        ResetPlayerHealth();
        UpdateHighscores(score);
        GiveExperience(score / 2);
        ResetScore();
    }


    public void StartDungeon() {
        FloorGenerator.floorNumber = 1;
        SceneManager.LoadScene(2);
    }

    public void SetMode(int key){
        mode = key;
    }

    public static void GiveScore(int addedScore) {
        score += addedScore;
    }

    public static void ResetScore(){
        score = 0;
        gold = 0;
        souls = 0;
        PlayerController.roomsCleared = 0;
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
        if(mode == 2) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void GiveExperience(int expToGive)
    {
        int exp = PlayerPrefs.GetInt("Experience", 0);
        exp += expToGive;
        PlayerPrefs.SetInt("Experience", exp);
    }

    public void GiveGold(int goldToGive)
    {
        int g = PlayerPrefs.GetInt("Gold", 0);
        g += goldToGive;
        PlayerPrefs.SetInt("Gold", g);
    }

    public void GiveSouls(int soulsToGive)
    {
        int s = PlayerPrefs.GetInt("Souls", 0);
        s += soulsToGive;
        PlayerPrefs.SetInt("Souls", s);
    }

    public void CountDeath(){
        int number = PlayerPrefs.GetInt("Deaths", 0);
        number++;
        PlayerPrefs.SetInt("Deaths", number);
    }

    public static void Win(){
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DropWin>().Drop();
    }

    public static void Death(){
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DropDeath>().Drop();
        
    }

    public static void ResetPlayerHealth() {
        try
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetStats();
        }
        catch { };
    }

    public static void DestroyPlayer() {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void ResetAllProgress() {
        PlayerPrefs.DeleteAll();
        Debug.Log("deleted");
    }

    public void SetDifficulty(int dif) {
        if(dif == 1)
        {
            difficulty = 1;
            PlayerController.baseHealth = 200;
            PlayerController.damageBoost = 10;
        }
        if(dif == 2)
        {
            difficulty = 2;
            PlayerController.baseHealth = 100;
            PlayerController.damageBoost = 0;
        }
        if(dif == 3)
        {
            difficulty = 3;
            PlayerController.baseHealth = 75;
            PlayerController.damageBoost = 0;
            
        }
    }

    public void QuitApp() {
        Application.Quit();
    }
}
