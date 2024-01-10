using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProperties", menuName = "TheColorBall / PlayerProperties", order = 0)]
public class PlayerProperties : ScriptableObject
{
    public GameObject bulletPrefab;
    public Color startColor = Color.white;
    public float movementBuffer = 10f;
    public float bulletSpeed = 0.3f;
    public float MaxBulletDistance = 10f;
    public float BulletIntravel = 2;
}