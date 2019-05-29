using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClear : MonoBehaviour
{

    private GameObject enemy;
    
    void Update()
    {
            enemy = GameObject.FindGameObjectWithTag("Enemy");

            if (enemy == null)
            {
                ClearRoom();
            }
        
    }

    private void ClearRoom(){
        PlayerController.roomsCleared += 1;
        Destroy(gameObject);
    }
}
