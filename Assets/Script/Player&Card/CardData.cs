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

    [Header("# ���� ����")]
    public CardType Type;
    public Color color;
    public TargetType targetType;
    public bool isUpgraded;
    public int cost;
    public int upgradeCost;
    public string name;
    [Header("# ī�� ����")]
    [TextArea]
    public string baseDesc;
    [TextArea]
    public string upgradeDesc;
    [Header("# ī���̹���")]
    public Sprite Icon;
    [Header("# ȿ����")]
    public Effect[] baseEffects;
    public Effect[] upgradeEffects;
}

[System.Serializable]
public class Effect
{
    public enum EffectType { Attack, Defence, Draw, StrengthBuff, Dexterity, RegenBuff, VulnerableBuff, WeakBuff, FrailBuff, ThornsBuff, BarricadeBuff, EnergizedBuff }
    /*
    Attack / ����: ���ݷ¸�ŭ �����մϴ�.
    Defence / ����: ���¸�ŭ ������ ä���ݴϴ�.
    Draw / ��ο�: ī�带 ��ο� �մϴ�.
    Strength / ��: �÷��̾��� ���� ī�尡 �߰� ���ظ� �����ϴ�.
    Dexterity / ��ø��: �÷��̾��� ��� ī�尡 �߰� ������ �����մϴ�.
    Regen / ���: �� �ϸ��� �÷��̾��� ü���� ȸ���˴ϴ�.
    Vulnerable / ���: �÷��̾�� �Դ� ���ذ� �����մϴ�.
    Weak / ��ȭ: �÷��̾��� ���� ���ذ� �����մϴ�.
    Frail / ����: �÷��̾��� ������ �����մϴ�.
    Thorns / ����: �÷��̾�� ������ ������ ���ظ� �ݻ��մϴ�.
    Barricade / �ٸ����̵�: �÷��̾��� ������ ���� ���� �� ������� �ʽ��ϴ�.
    Energized / Ȱ��: ���� �Ͽ� �÷��̾��� �������� �����մϴ�.
    */
    public EffectType effectType;
    public int amount;
}
