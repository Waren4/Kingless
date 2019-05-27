using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private bool paused = false;
    public GameObject pauseMenu;
    public GameObject pauseBackground;

	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            ManagePause(!paused);
        }	
	}

    public void ManagePause(bool pause)
    {
        if(pause)
        {
            pauseMenu.SetActive(true);
            pauseBackground.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            pauseBackground.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
        }
    }
}
