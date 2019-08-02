using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWin : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject pauseBackground;

    public void Drop()
    {
        pauseBackground.SetActive(true);
        winScreen.SetActive(true);
    }
}
