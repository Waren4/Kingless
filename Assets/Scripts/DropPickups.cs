using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickups : MonoBehaviour
{
    [Header ("Drops")]
    public GameObject soul;
    public GameObject coin;

    [Header("Coin range")]
    public float coinRange1;
    public float coinRange2;
    public int coinNumber1, coinNumber2, coinNumber3;

    [Header("Soul range")]
    public float soulRange1;
    public float soulRange2;
    public int soulNumber1, soulNumber2, soulNumber3;

    [Header("Drop Radius")]
    public float dropX, dropY;

    public void DropItems() {

        float r;
        int dropped = 0;

        r = Random.Range(0f, 1f);

        if (r > soulRange1) {
            if (r  > soulRange2) {
                Drop(soul, soulNumber3);
                dropped = soulNumber3;
            }
            else {
                Drop(soul, soulNumber2);
                dropped = soulNumber2;
            }
        }
        else {
            Drop(soul, soulNumber1);
            dropped = soulNumber1;
        }

        r = Random.Range(0f, 1f);

        if (dropped == 0)
        {
            if (r > coinRange1)
            {
                if (r > coinRange2)
                {
                    Drop(coin, coinNumber3);
                }
                else
                {
                    Drop(coin, coinNumber2);
                }
            }
            else
            {
                Drop(coin, coinNumber1);
            }
        }

    }

    private void Drop(GameObject obj, int number) {
        int i;
        float x, y;
        

        Vector3 position;

        for(i = 1; i <= number; ++i) {

            x = Random.Range(-dropX, dropX);
            y = Random.Range(-dropY, dropY);

            position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

            Instantiate(obj, position, Quaternion.identity);
        }
    }
}
