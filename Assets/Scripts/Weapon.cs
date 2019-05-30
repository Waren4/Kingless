using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    // Order: Basic Sword, Glow Sword Blue, ...
    public static int[] damageValues = { 15, 20 };
    public static float[] ranges = { 0.9f, 0.9f };
    public static float[] knockbacks = { 1.5f, 1f };

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
