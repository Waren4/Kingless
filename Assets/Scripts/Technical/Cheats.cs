using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private float timer = 0f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            if (timer <= 0f) {
                GameManager.NextFloor();
                timer = 2f;
            }

        }
        
        if (timer > 0f) timer -= Time.deltaTime;

    }
}
