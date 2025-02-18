using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave {
    public string Name = "Wave";
    public float DelayInSeconds;
    public float IntervalInSeconds;
    public List<FishSO> FishesInOrder;
}

[CreateAssetMenu(fileName = "RoundSO_", menuName = "ScriptableObjects/RoundSO")]
public class RoundSO : ScriptableObject
{
    public string Name;
    
    public List<Wave> Waves;
}