using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{

    public GameObject fadeImage;

    public void Fade() {
        fadeImage.SetActive(true);
    }
}
