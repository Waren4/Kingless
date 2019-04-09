using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Order: Glow Sword Blue, ...
    public static int[] damageValues = { 12 };
    public static float[] ranges = { 1f };

    public float animatorIndex;
    public int damage;
    public float range;

    private void Start() {
        int i = (int)animatorIndex;
        damage = damageValues[i];
        range = ranges[i];
    }
}
