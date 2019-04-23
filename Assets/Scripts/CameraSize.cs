using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    public float screenRatio;
    public float targetRatio;
    void Start()
    {
        float targetWidth = 960.0f;
        float targetHeight = 540.0f;

        float desiredRatio = targetWidth / targetHeight;
        float currentRatio = (float)Screen.width / (float)Screen.height;

        if (currentRatio >= desiredRatio) {
            Camera.main.orthographicSize = targetHeight / 2f / 32f;
        }
        else {
            float differenceInSize = desiredRatio / currentRatio;
            Camera.main.orthographicSize = targetHeight / 2f / 32f * differenceInSize;
        }
    }

}
