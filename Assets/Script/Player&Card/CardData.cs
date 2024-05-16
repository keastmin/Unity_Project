using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "cardData", menuName = "Scriptable object/CardData")]

public class CardData : ScriptableObject
{
    public enum CardType { Attack, Skill, Power }
    public enum CardRank { Basic, Special, Rarity }
    public enum Color { Red, Gray, Blue }
    public enum TargetType { None, One, Random, All }

    [Header("# 메인 정보")]
    public CardType Type;
    public Color color;
    public TargetType targetType;
    public bool isUpgraded;
    public int cost;
    public int upgradeCost;
    public string name;
    [Header("# 카드 설명")]
    [TextArea]
    public string baseDesc;
    [TextArea]
    public string upgradeDesc;
    [Header("# 카드이미지")]
    public Sprite Icon;
    [Header("# 효과들")]
    public Effect[] baseEffects;
    public Effect[] upgradeEffects;
}

[System.Serializable]
public class Effect
{
    public enum EffectType { Attack, Defence, Draw, StrengthBuff, Dexterity, RegenBuff, VulnerableBuff, WeakBuff, FrailBuff, ThornsBuff, BarricadeBuff, EnergizedBuff }
    /*
    Attack / 공격: 공격력만큼 공격합니다.
    Defence / 방어력: 방어력만큼 방어력을 채워줍니다.
    Draw / 드로우: 카드를 드로우 합니다.
    Strength / 힘: 플레이어의 공격 카드가 추가 피해를 입힙니다.
    Dexterity / 민첩성: 플레이어의 방어 카드가 추가 방어력을 제공합니다.
    Regen / 재생: 매 턴마다 플레이어의 체력이 회복됩니다.
    Vulnerable / 취약: 플레이어에게 입는 피해가 증가합니다.
    Weak / 약화: 플레이어의 공격 피해가 감소합니다.
    Frail / 연약: 플레이어의 방어력이 감소합니다.
    Thorns / 가시: 플레이어에게 공격한 적에게 피해를 반사합니다.
    Barricade / 바리케이드: 플레이어의 방어력이 턴이 끝날 때 사라지지 않습니다.
    Energized / 활력: 다음 턴에 플레이어의 에너지가 증가합니다.
    */
    public EffectType effectType;
    public int amount;
}
