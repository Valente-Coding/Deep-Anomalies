using UnityEngine;

[CreateAssetMenu(fileName = "FishSO_", menuName = "ScriptableObjects/FishSO")]
public class FishSO : ScriptableObject
{
    public string Name;
    public Sprite FishSprite;
    public float Speed = 1f;
    public int Value = 1;
}