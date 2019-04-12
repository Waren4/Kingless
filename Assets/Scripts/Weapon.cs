using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Order: Glow Sword Blue, ...
    public static int[] damageValues = { 12 };
    public static float[] ranges = { 0.85f };
    public static float[] knockbacks = { 1f };

    public float animatorIndex;
    public int damage;
    public float range;
    public float knockback;

    private void Start() {
        int i = (int)animatorIndex;
        damage = damageValues[i];
        range = ranges[i];
        knockback = knockbacks[i];
    }
}
