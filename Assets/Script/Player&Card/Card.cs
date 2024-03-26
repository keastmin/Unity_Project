using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�� ������ ��Ÿ���� ������
public enum CardType
{
    Attack, // ���� ī��
    Skill, // ��ų ī��
    Power, // �Ŀ� ī��
    Curse // ���� ī��
}

// ī�� Ŭ����
public class Card : ScriptableObject
{
    public string cardName; // ī���� �̸�
    public string description; // ī���� ����   
    public int cost; // ī���� ���  
    public Sprite artwork; // ī���� ��Ʈ��ũ(�̹���)   
    public CardType type; // ī���� ����
    public PlayerSc playersc;

    // ī�� �Ӽ��� �ʱ�ȭ�ϴ� ������
    public Card(string name, string desc, int cost, Sprite art, CardType type)
    {
        this.cardName = name;
        this.description = desc;
        this.cost = cost;
        this.artwork = art;
        this.type = type;
    }

    // ī�带 ����ϴ� �޼���
    public virtual void Play()
    {
        Debug.Log("ī�� ���: " + cardName);
        // ���⼭ ī�� ȿ���� �����մϴ�.
    }
}

public class AttackCard : Card
{
    public int damage; // ���ݷ�

    // AttackCard ������
    public AttackCard(string name, string desc, int cost, Sprite art, int damage)
        : base(name, desc, cost, art, CardType.Attack)
    {
        this.damage = damage;
    }

    // AttackCard�� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("���ݷ�: " + damage);
        // ���⼭ �߰����� ���� ī�� ȿ���� �����մϴ�.
    }
}

// ��ų ī�� Ŭ����
public class SkillCard : Card
{
    // ��ų ī�忡 ���� Ưȭ�� �Ӽ���
    public int effectValue; // ��ų ȿ�� ��

    // ��ų ī�� ������
    public SkillCard(string name, string desc, int cost, Sprite art, int effectValue)
        : base(name, desc, cost, art, CardType.Skill)
    {
        this.effectValue = effectValue;
    }

    // ��ų ī���� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("��ų ȿ�� ��: " + effectValue);
        // ���⼭ �߰����� ��ų ī�� ȿ���� �����մϴ�.
    }
}

// �Ŀ� ī�� Ŭ����
public class PowerCard : Card
{
    // �Ŀ� ī�忡 ���� Ưȭ�� �Ӽ���
    public int buffValue; // �Ŀ� ȿ�� ��

    // �Ŀ� ī�� ������
    public PowerCard(string name, string desc, int cost, Sprite art, int buffValue)
        : base(name, desc, cost, art, CardType.Power)
    {
        this.buffValue = buffValue;
    }

    // �Ŀ� ī���� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("�Ŀ� ȿ�� ��: " + buffValue);
        // ���⼭ �߰����� �Ŀ� ī�� ȿ���� �����մϴ�.
    }
}

// ���� ī�� Ŭ����
public class CurseCard : Card
{
    // ���� ī�忡 ���� Ưȭ�� �Ӽ���
    public int curseValue; // ���� ȿ�� ��

    // ���� ī�� ������
    public CurseCard(string name, string desc, int cost, Sprite art, int curseValue)
        : base(name, desc, cost, art, CardType.Curse)
    {
        this.curseValue = curseValue;
    }

    // ���� ī���� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("���� ȿ�� ��: " + curseValue);
        // ���⼭ �߰����� ���� ī�� ȿ���� �����մϴ�.
    }
}