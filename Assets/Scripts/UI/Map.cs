using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

    public GameObject d, dl, dlu, dlur, dlr, du, dur, dr, l, lu, lur, lr, u, ur, r;

    private Room[,] rooms = new Room[42, 42];

    private void Start() {
        Invoke("CreateMap", 2f);     
    }

    public void GetRoom(Room roomScript, float x, float y) {
        x = x / 30;
        y = y / 17;
        if (x <= 0f)
        {
            x = -x;
            x = 20 - x;
        }
        else x = x + 20;
        if (y <= 0f)
        {
            y = -y;
            y = y + 20;
        }
        else y = 20 - y;
        rooms[(int)y,(int)x] = roomScript;
    }

    private void CreateMap() {
        bool D, L, U, R;
        Room room = null;
        int i, j;
        for(i = 1; i <= 40; ++i) {
            for(j = 1; j <= 40; ++j) {
                if (rooms[i, j] == null) continue;
                D = false; L = false; U = false; R = false;
                room = rooms[i,j];

                if (room.exitDown) {
                    if (rooms[i + 1, j] != null && rooms[i + 1, j].exitUp) D = true;
                }
                if(room.exitLeft) {
                    if (rooms[i, j - 1] != null && rooms[i, j - 1].exitRight) L = true;
                }
                if(room.exitUp) {
                    if (rooms[i - 1, j] != null && rooms[i - 1, j].exitDown) U = true;
                }
                if(room.exitRight) {
                    if (rooms[i, j + 1] != null && rooms[i, j + 1].exitLeft) R = true;
                }

                CreateRoom(D, L, U, R, i, j);
            }
        }
    }

    private void CreateRoom(bool D, bool L, bool U, bool R, int x, int y) {
        Vector3 position = new Vector3(transform.position.y + y, transform.position.x - x * 0.5625f, transform.position.z);
        GameObject obj = null;

        //Debug.Log(x); Debug.Log(y); Debug.Log(D); Debug.Log(L); Debug.Log(U); Debug.Log(R);

        if (D) {
            if (L)
            {
                if (U)
                {
                    if (R)
                    {
                        obj = dlur;
                    }
                    else {
                        obj = dlu;
                    }
                }
                else {
                    if (R)
                    {
                        obj = dlr;
                    }
                    else
                    {
                        obj = dl;
                    }
                }
            }
            else
            {
                if (U)
                {
                    if (R)
                    {
                        obj = dur;
                    }
                    else
                    {
                        obj = du;
                    }
                }
                else
                {
                    if (R)
                    {
                        obj = dr;
                    }
                    else
                    {
                        obj = d;
                    }
                }
            }
        }
        else
        {
            if (L)
            {
                if (U)
                {
                    if (R)
                    {
                        obj = lur;
                    }
                    else
                    {
                        obj = lu;
                    }
                }
                else
                {
                    if (R)
                    {
                        obj = lr;
                    }
                    else
                    {
                        obj = l;
                    }
                }
            }
            else
            {
                if (U)
                {
                    if (R)
                    {
                        obj = ur;
                    }
                    else
                    {
                        obj = u;
                    }
                }
                else
                {
                    if (R)
                    {
                        obj = r;
                    }
                }
            }
        }

        Instantiate(obj, position, Quaternion.identity, transform);


    }
}
