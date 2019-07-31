using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Image startRoom;
    public Transform mapObject;

    private Image[,] map;
    private GameObject imageObject;

    private void Start() {
        map = new Image[31,31];
        


        map[15,15] = Instantiate(startRoom, new Vector3(64 * 15, 36 * 15, 0), Quaternion.identity, gameObject.transform) as Image;
        
    }

    public void SetRoomImage(Image img,int x,int y){

        map[x, y] = img;
        Instantiate(map[x, y], new Vector3(64 * x, 36 * y, 0), Quaternion.identity, gameObject.transform);
    }

    public void EnableRoomImage(int x,int y){
        map[x, y].enabled = true;
    }
}
