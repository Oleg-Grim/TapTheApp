using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Patterns
{
    public enum Direction { clockwise, counterclockwise };
    public enum Movetype { accelerate, decelerate };

    [Header("Phase configuration")]

    public Direction direction;
    public float duration;
    public Movetype movetype;
    [Range(0f, 100f)]
    public float speedModifier;
}

[System.Serializable]
public class Levels
{
    public bool BossLevel;
    //public Sprite sprite;
    //public Material shatterMat;
    public int levelIndex;
    public int hitCouter;
    [Range(0,3)] public int stuckCount;
    [Range(0,3)] public int adsCount;
    [Range(0, 1)] public float adChance;

    [Range(0, 10)]
    public float maxSpeed;
    public Patterns[] levelPattern;
}   
