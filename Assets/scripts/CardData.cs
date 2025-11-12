using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card System/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    [TextArea] public string description;
    public Sprite cardImage;
    [Range(0.1f, 10f)] public float weight = 1f; // อัตราการออก

    public CardEffect[] effects;
}

[System.Serializable]
public class CardEffect
{
    public TargetType target; // Player หรือ Enemy
    public StatType stat;
    public float value;
    public bool isPercentage; // true = คิดเป็น %
}

public enum TargetType { Player, Enemies }
public enum StatType { DMG, SpeedAttack, Range, HP, Speed, ScoreValue }