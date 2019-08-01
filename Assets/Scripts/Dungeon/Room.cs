using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int exitNumber;
    public bool exitDown, exitLeft, exitUp, exitRight;
    //   public Vector2 position;


    private Map mapScript;

    private void Start()
    {
        mapScript = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();

        mapScript.GetRoom(this, transform.position.x, transform.position.y);
    }

}
