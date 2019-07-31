using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFloat : MonoBehaviour
{

    //private float range = 0.2f;
    private float relativeY = 0f;
    private bool up = true;

    void Update() {
        if (relativeY > 0.2f) up = false;
        if (relativeY < 0f) up = true;
        if(up) {
            relativeY += Time.deltaTime;
            transform.Translate(new Vector3(0f, Time.deltaTime/2, 0f));
        }
        else {
            relativeY -= Time.deltaTime;
            transform.Translate(new Vector3(0f, -Time.deltaTime/2, 0f));
        }
    }
}
