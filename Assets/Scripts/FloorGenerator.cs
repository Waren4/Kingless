﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public int floorNumber = 1;

    public Transform grid;
    public GameObject[] roomsD, roomsL, roomsU, roomsR;

    

    private int numberOfRooms;
    private Queue<RoomPosition> q;
    private List<Vector2> takenPositions;


    private void Awake() {
        switch (floorNumber)
        {
            case 1:
                numberOfRooms = 12;
                break;
            default:
                break;
        }
        q = new Queue<RoomPosition>();
        takenPositions = new List<Vector2>();
    }


    private void Start() {

        EnqueueEntryRoom();

        while (q.Count > 0) {

            RoomPosition rp = q.Peek();
            q.Dequeue();

            int i;
            GameObject room;
            Vector3 position;
            

            switch (rp.type)
            {
                case 'D':

                    i = RandomRoomDown();
                    room = roomsU[i];
                    position = new Vector3(rp.position.x, rp.position.y,0f);

                    EnqueueRooms(room, rp);

                    Instantiate(room, position, Quaternion.identity, grid);

                    break;
                case 'L':

                    i = RandomRoomLeft();
                    room = roomsR[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    Instantiate(room, position, Quaternion.identity, grid);

                    break;
                case 'U':

                    i = RandomRoomUp();
                    room = roomsD[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    Instantiate(room, position, Quaternion.identity, grid);

                    break;
                case 'R':

                    i = RandomRoomRight();
                    room = roomsL[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    Instantiate(room, position, Quaternion.identity, grid);

                    break;
                default:
                    break;
            }
        }
    }


    private void EnqueueEntryRoom() {
        RoomPosition rp;
        rp = new RoomPosition(0f,-17f,'D');
        q.Enqueue(rp);
        rp = new RoomPosition(-30f, 0f, 'L');
        q.Enqueue(rp);
        rp = new RoomPosition(0f, 17f, 'U');
        q.Enqueue(rp);
        rp = new RoomPosition(30f, 0f, 'R');
        q.Enqueue(rp);
        numberOfRooms -= 4;
        takenPositions.Add(new Vector2(0f, 0f));
        takenPositions.Add(new Vector2(0f, -17f));
        takenPositions.Add(new Vector2(0f, 17f));
        takenPositions.Add(new Vector2(-30f, 0f));
        takenPositions.Add(new Vector2(30f, 0f));
    }

    private void EnqueueRooms(GameObject room, RoomPosition rp)
    {
        Room roomScript = room.GetComponent<Room>();
        if (roomScript.exitDown)
        {
            Vector2 newRoomPos = new Vector2(rp.position.x, rp.position.y - 17f);
            if (!takenPositions.Contains(newRoomPos))
            {
                RoomPosition newRp = new RoomPosition(newRoomPos.x, newRoomPos.y, 'D');
                q.Enqueue(newRp);
                --numberOfRooms;
                takenPositions.Add(newRoomPos);
            }
        }
        if (roomScript.exitLeft)
        {
            Vector2 newRoomPos = new Vector2(rp.position.x - 30f, rp.position.y);
            if (!takenPositions.Contains(newRoomPos))
            {
                RoomPosition newRp = new RoomPosition(newRoomPos.x, newRoomPos.y, 'L');
                q.Enqueue(newRp);
                --numberOfRooms;
                takenPositions.Add(newRoomPos);
            }
        }
        if (roomScript.exitUp)
        {
            Vector2 newRoomPos = new Vector2(rp.position.x, rp.position.y + 17f);
            if (!takenPositions.Contains(newRoomPos))
            {
                RoomPosition newRp = new RoomPosition(newRoomPos.x, newRoomPos.y, 'U');
                q.Enqueue(newRp);
                --numberOfRooms;
                takenPositions.Add(newRoomPos);
            }
        }
        if (roomScript.exitRight)
        {
            Vector2 newRoomPos = new Vector2(rp.position.x + 30f, rp.position.y);
            if (!takenPositions.Contains(newRoomPos))
            {
                RoomPosition newRp = new RoomPosition(newRoomPos.x, newRoomPos.y, 'R');
                q.Enqueue(newRp);
                --numberOfRooms;
                takenPositions.Add(newRoomPos);
            }
        }
    }

    private int RandomRoomDown()
    {
        int i;

        i = Random.Range(0, roomsU.Length);
        if (q.Count == 0 && numberOfRooms > 0)
        {
            while (roomsU[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms || roomsU[i].GetComponent<Room>().exitNumber == 1)
            {
                i = Random.Range(0, roomsU.Length);
            }
        }
        else
        {
            while (roomsU[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms)
            {
                i = Random.Range(0, roomsU.Length);
            }
        }
        return i;
    }

    private int RandomRoomLeft() {
        int i;
        i = Random.Range(0, roomsR.Length);
        if (q.Count == 0 && numberOfRooms > 0)
        {
            while (roomsR[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms || roomsR[i].GetComponent<Room>().exitNumber == 1)
            {
                i = Random.Range(0, roomsR.Length);
            }
        }
        else
        {
            while (roomsR[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms)
            {
                i = Random.Range(0, roomsR.Length);
            }
        }
        return i;
    }

    private int RandomRoomUp() {
        int i;
        i = Random.Range(0, roomsD.Length);
        if (q.Count == 0 && numberOfRooms > 0)
        {
            while (roomsD[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms || roomsD[i].GetComponent<Room>().exitNumber == 1)
            {
                i = Random.Range(0, roomsD.Length);
            }
        }
        else
        {
            while (roomsD[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms)
            {
                i = Random.Range(0, roomsD.Length);
            }
        }
        return i;
    }

    private int RandomRoomRight() {
        int i;
        i = Random.Range(0, roomsL.Length);
        if (q.Count == 0 && numberOfRooms > 0)
        {
            while (roomsL[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms || roomsL[i].GetComponent<Room>().exitNumber == 1)
            {
                i = Random.Range(0, roomsL.Length);
            }
        }
        else
        {
            while (roomsL[i].GetComponent<Room>().exitNumber - 1 > numberOfRooms)
            {
                i = Random.Range(0, roomsL.Length);
            }
        }
        return i;
    }

    private class RoomPosition
    {
        public Vector2 position;
        public char type;

        public RoomPosition(float x, float y, char t)
        {
            position.x = x;
            position.y = y;
            type = t;
        }
    }

    

}