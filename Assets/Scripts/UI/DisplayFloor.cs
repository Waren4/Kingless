using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFloor : MonoBehaviour
{
    private TextMeshProUGUI floorText;

    void Start()
    {
        floorText = GetComponent<TextMeshProUGUI>();

        SetFloor();
    }


    private void SetFloor()
    {
        if(GameManager.mode == 2) floorText.text = "Floor " + FloorGenerator.floorNumber.ToString();
        else {
            if (FloorGenerator.floorNumber == 1) floorText.text = "Cave";
            if (FloorGenerator.floorNumber == 2) floorText.text = "Catacombs";
            if (FloorGenerator.floorNumber == 3) floorText.text = "Castle";
            if (FloorGenerator.floorNumber == 4) floorText.text = "Throne Room";
        }
    }
}
