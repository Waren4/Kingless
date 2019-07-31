using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int exitNumber;
    public bool exitDown, exitLeft, exitUp, exitRight;
    public Vector2 position;

    private string roomCode;
//    public GameObject blockDownDoor, blockLeftDoor, blockUpDoor, blockRightDoor;

    private void Start()
    {

        roomCode = gameObject.name;
      //  blockDownDoor.SetActive(false);
       // blockLeftDoor.SetActive(false);
        //blockRightDoor.SetActive(false);
       // blockUpDoor.SetActive(false);
    }

}
