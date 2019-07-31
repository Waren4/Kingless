using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFloorClear : MonoBehaviour
{

    public GameObject exit;

    void Update(){
        if (PlayerController.roomsCleared == FloorGenerator.totalRoomNumber){
            exit.SetActive(true);
        }
    }
}
