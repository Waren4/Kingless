using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorGenerator : MonoBehaviour
{
    public static int floorNumber = 1;
    public static int totalRoomNumber;

    [Header ("Grid")]
    public Transform grid;
    [Header ("Empty rooms")]
    public GameObject[] roomsD, roomsL, roomsU, roomsR;
    [Header ("Room Decorations")]
    public GameObject[] decorations;
    [Header("Enemy Groups")]
    public GameObject[] enemyGroups;

    private int[] roomNumber = { 0,6,10,14,18,22,25,28,33,36,40,45,50,54,60,63,67,70,73,77,80,85,90,95,100, 110, 115, 120, 130, 140, 150 };
    private int numberOfRooms;
    private Queue<RoomPosition> q;
    private List<Vector2> takenPositions;

    private List<int> usedRooms;

    private void Awake() {

        if(GameManager.mode == 2) numberOfRooms = roomNumber[floorNumber];
        else {
            if (floorNumber == 1) numberOfRooms = 8;
            if (floorNumber == 2) numberOfRooms = 12;
            if (floorNumber == 3) numberOfRooms = 15;
        }
        totalRoomNumber = numberOfRooms;

        q = new Queue<RoomPosition>();
        takenPositions = new List<Vector2>();
        usedRooms = new List<int>();
        
    }


    private void Start() {

        EnqueueEntryRoom();

        while (q.Count > 0) {

            RoomPosition rp = q.Peek();
            q.Dequeue();

            int i;
            GameObject room;
            GameObject roomInstance;
            Vector3 position;
            

            switch (rp.type)
            {
                case 'D':

                    i = RandomRoomDown();
                    room = roomsU[i];
                    position = new Vector3(rp.position.x, rp.position.y,0f);

                    EnqueueRooms(room, rp);

                    roomInstance = Instantiate(room, position, Quaternion.identity, grid) as GameObject;
                    

                    GenerateRandomLayout(roomInstance, position);

                    break;
                case 'L':

                    i = RandomRoomLeft();
                    room = roomsR[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    roomInstance = Instantiate(room, position, Quaternion.identity, grid) as GameObject;
                    

                    GenerateRandomLayout(roomInstance, position);

                    break;
                case 'U':

                    i = RandomRoomUp();
                    room = roomsD[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    roomInstance = Instantiate(room, position, Quaternion.identity, grid) as GameObject;
                    

                    GenerateRandomLayout(roomInstance, position);

                    break;
                case 'R':

                    i = RandomRoomRight();
                    room = roomsL[i];
                    position = new Vector3(rp.position.x, rp.position.y, 0f);

                    EnqueueRooms(room, rp);

                    roomInstance = Instantiate(room, position, Quaternion.identity, grid) as GameObject;
                    

                    GenerateRandomLayout(roomInstance, position);

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

    private void GenerateRandomLayout(GameObject parent, Vector3 position){
        int i;
        //GameObject spawnpoint;
        GameObject decorationsInstance;

        i = Random.Range(0, decorations.Length);
        if (GameManager.mode == 1) {
            while (usedRooms.Contains(i))
            {
                i = Random.Range(0, decorations.Length);
            }
            usedRooms.Add(i);
        }
        decorationsInstance = Instantiate(decorations[i], position,Quaternion.identity,parent.transform);

        // spawnpoint = decorationsInstance.GetComponent<SpawnPointTransform>().spawnPoint;

        // i = Random.Range(0, enemyGroups.Length);
        Instantiate(enemyGroups[i],parent.transform.position,Quaternion.identity, parent.GetComponent<EnemiesLocation>().enemies.transform);
    }

    
}
