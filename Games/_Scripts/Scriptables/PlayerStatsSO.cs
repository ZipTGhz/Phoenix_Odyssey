using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO")]
public class PlayerStatsSO : ScriptableObject
{
    //STATS INFO
    [SerializeField]
    Stats _stats;

    public Stats BaseStats
    {
        get => _stats;
    }
}

[Serializable]
public class Stats
{
    public float Heath;
    public int Mana;
    public int Level;
    public int LevelProgress;
}
