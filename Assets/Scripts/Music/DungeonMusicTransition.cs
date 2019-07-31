using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMusicTransition : MonoBehaviour
{

    private static DungeonMusicTransition instance;

    private void Awake() {

        Destroy(GameObject.FindGameObjectWithTag("MenuMusic"));
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }

    }
}
