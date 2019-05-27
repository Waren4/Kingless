using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeath : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject pauseBackground;
    
    public void Drop(){
        pauseBackground.SetActive(true);
        deathScreen.SetActive(true);
    }
}
