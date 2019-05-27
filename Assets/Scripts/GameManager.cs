using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int score = 0;

    public void ExitToHub() {
        SceneManager.LoadScene(1);
    }

    public void ExitToMenu() {
        SceneManager.LoadScene(0);
    }

    public static void NextFloor(){
        FloorGenerator.floorNumber += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Death(){
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<DropDeath>().Drop();
    }
}
