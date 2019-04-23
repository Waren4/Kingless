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
                OpenDoors();
            }
        
    }

    private void OpenDoors(){
        Destroy(gameObject);
    }
}
