using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject emptyRoom;
    public GameObject grid;
    public GameObject empty;
    public GameObject[] rooms;
    public GameObject[] enemyGroups;
    

    private int x = 0;
    private int y = -17;

    private void Start() {

        Generate();
        
    }

    private void Generate() {
        int i, j, n, m;
        n = rooms.Length;
        m = enemyGroups.Length;
        for(i = 0; i < n; ++i) {
            for(j = 0; j < m; ++j) {
                GameObject instance;
                instance = Instantiate(empty, new Vector3(x, y, 0), Quaternion.identity,grid.transform) as GameObject;
                Instantiate(emptyRoom, new Vector3(x, y, 0), Quaternion.identity, instance.transform);

                

                Instantiate(rooms[i], instance.transform.position, Quaternion.identity, instance.transform);
                Instantiate(enemyGroups[j], instance.transform.position, Quaternion.identity, instance.transform);

                x += 30;
            }
            x = 0;
            y -= 17;
        }
    }
}
