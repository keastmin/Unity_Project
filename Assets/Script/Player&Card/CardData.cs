using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "cardData", menuName = "Scriptable object/CardData")]
// ����Ƽ �����Ϳ��� Assets �޴��� ���ο� �޴� �׸��� �߰��� �� �ֽ��ϴ�.
// �� �׸��� �����ϸ� ������ ���� �̸��� ��ο� ���ο� ScriptableObject �ν��Ͻ��� ������ �� �ֽ��ϴ�.
[System.Serializable] // ����Ƽ �����Ϳ��� ������ ������ ������ �����ϰ� ������ �� �ֵ��� �մϴ�.
public class CardData : ScriptableObject
{
    public enum CardType { Attack, Skill, Power }
    public enum CardRank { Basic, Special, Rarity }
    public enum Color { Red, Gray, Blue }
    public enum TargetType { None, One, Random, All }

    [Header("# ���� ����")]
    public CardType Type; // ī���� Ÿ���� ���մϴ�.
    public Color color; // ī���� �÷�(��͵�) 
    public TargetType targetType; // ī���� Ÿ��Ÿ��
    public bool isUpgraded; // ī�尡 ���׷��̵� �Ǿ����� �Һ���
    public int cost; // ī���� �ڽ�Ʈ
    public int upgradeCost; // ���׷��̵� �� �ڽ�Ʈ
    public string cardName; // ī�� �̸�
    [Header("# ī�� ����")]
    [TextArea]
    public string baseDesc; // ī�� �⺻ ����
    [TextArea] 
    public string upgradeDesc; // ���׷��̵� �� ����
    [Header("# ī���̹���")]
    public Sprite Icon; // ī�� �̹���
    [Header("# ȿ����")]
    public Effect[] baseEffects; // ī�� ȿ��
    public Effect[] upgradeEffects; // ���׷��̵� �� ȿ��
}

[System.Serializable]
public class Effect // ȿ�� ����
{
    public enum EffectType { Attack, Defence, Heal, Draw, StrengthBuff, Dexterity, RegenBuff, VulnerableBuff
            ,WeakBuff, FrailBuff, ThornsBuff, BarricadeBuff, EnergizedBuff, Extinction, Condition }
    /*
    Attack / ����: ���ݷ¸�ŭ �����մϴ�.
    Defence / ����: ���¸�ŭ ������ ä���ݴϴ�.
    Heal / ��: ü���� ȸ���մϴ�(������ ����)
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
    Extinction / �Ҹ�: ī�� ���� �Ҹ�ȴ�.
    Condition / ����: Ư���� ������ ���� ���� �ߵ�.
    */
    public EffectType effectType; // ȿ�� Ÿ��
    public int amount; // ȿ�� ũ��
}
