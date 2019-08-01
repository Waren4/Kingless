using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    private Transform mapLocation;
    private Vector3 offset;

    private Vector3 oldLocation;

    private PlayerController playerScript;

    private bool showingMap;
    private bool hasMap;

    private void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerScript = player.GetComponent<PlayerController>();
        

        mapLocation = GameObject.FindGameObjectWithTag("Map").transform;
        offset = new Vector3(20f, -11.25f, -10f);
        showingMap = false;

        if (PlayerPrefs.GetInt("HasMap", 0) == 1) hasMap = true;
        else hasMap = false;
    }

    private void Update()
    {
        if (hasMap)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ManageMap(showingMap);
            }
        }
    }

    private void ManageMap(bool isShowing)
    {
        if(isShowing)
        {
            playerScript.enabled = true;
            Time.timeScale = 1f;
            transform.position = oldLocation;
            showingMap = false;
        }
        else {
            oldLocation = transform.position;
            playerScript.enabled = false;
            Time.timeScale = 0f;
            transform.position = mapLocation.position + offset;
            showingMap = true;
        }
    }

}
