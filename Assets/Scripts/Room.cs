using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int exitNumber;
    public bool exitDown, exitLeft, exitUp, exitRight;
    public Vector2 position;

//    public GameObject blockDownDoor, blockLeftDoor, blockUpDoor, blockRightDoor;

    private void Start()
    {
      //  blockDownDoor.SetActive(false);
       // blockLeftDoor.SetActive(false);
        //blockRightDoor.SetActive(false);
       // blockUpDoor.SetActive(false);
    }

}
