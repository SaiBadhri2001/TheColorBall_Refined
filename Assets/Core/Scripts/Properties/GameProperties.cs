using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameProperties", menuName = "TheColorBall / GameProperties", order = 1)]
public class GameProperties : ScriptableObject
{
    public PlayerType playerType;
    public float initialMovementSpeed = 1f;
    public float speedMultiplier = 1f;
    public int maxMovementSpeed = 8;
    public List<Color> totalColors;
    public List<GameObject> Levels;
    public int targetFrameRate = 60;
    public int totalLevelsAtTime = 5;
    public int levelsRunsBelow = 2;
    public int IntialBulletDamage = 10;
    public float _intensity = 5;
}
public enum PlayerType
{
    Normal,
    Debug
}