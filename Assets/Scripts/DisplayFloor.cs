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
        floorText.text = "Floor " + FloorGenerator.floorNumber.ToString();
    }
}
