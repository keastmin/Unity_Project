using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ī�� ������ ��Ÿ���� ������


// ī�� Ŭ����
public class NoUseCard : MonoBehaviour
{
    public string cardName; // ī���� �̸�
    public string description; // ī���� ����   
    public int cost; // ī���� ���  
    public Sprite artwork; // ī���� ��Ʈ��ũ(�̹���)   
    public CardType type; // ī���� ����
    public PlayerSc playersc; // �÷��̾� ��ũ��Ʈ ����
    public List<GameObject> Target = new List<GameObject>(); // ��� ����Ʈ

    private void Start()
    {
        // PlayerSc ��ũ��Ʈ�� �ν��Ͻ��� ã��
        playersc = FindObjectOfType<PlayerSc>();
        if (playersc == null)
        {
            Debug.Log("��ã��");
        }
        else
            Debug.Log("ã��" + playersc.gameObject.name + "/" + playersc.energy + "/" + cost);

        this.gameObject.GetComponent<Image>().sprite = artwork;
    }


    // ī�� �Ӽ��� �ʱ�ȭ�ϴ� ������
    public NoUseCard(string name, string desc, int cost, CardType type)
    {
        this.cardName = name;
        this.description = desc;
        this.cost = cost;
        this.type = type;
    }

    // ī�带 ����ϴ� �޼���
    public virtual void Play()
    {
        playersc.energy -= cost;
        Debug.Log("ī�� ���: " + cardName);
    }

    public bool isUsable()
    {
        Debug.Log("isUsable����");
        if (playersc.energy >= cost)
        {
            Debug.Log("������ ���� -> ����մϴ�.");
            return true;
        }
        Debug.Log("������ ������ -> �����մϴ�.");
        return false;
    }
}

public class AttackCard : NoUseCard
{
    public int damage; // ���ݷ�

    // AttackCard ������
    public AttackCard(string name, string desc, int cost, int damage)
        : base(name, desc, cost, CardType.Attack)
    {
        this.damage = damage;
    }

    // AttackCard�� Play �޼��带 �������մϴ�.
    public override void Play() 
    {
        Debug.Log("Attack Play ����" + isUsable());
        bool check = isUsable();
        Debug.Log("check��");
        if (check)
        {
            base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        }
    }
}

// ��ų ī�� Ŭ����
public class SkillCard : NoUseCard
{
    // ��ų ī�忡 ���� Ưȭ�� �Ӽ���
    public int effectValue; // ��ų ȿ�� ��

    // ��ų ī�� ������
    public SkillCard(string name, string desc, int cost, int effectValue)
        : base(name, desc, cost, CardType.Skill)
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
public class PowerCard : NoUseCard
{
    // �Ŀ� ī�忡 ���� Ưȭ�� �Ӽ���
    public int buffValue; // �Ŀ� ȿ�� ��

    // �Ŀ� ī�� ������
    public PowerCard(string name, string desc, int cost, int buffValue)
        : base(name, desc, cost, CardType.Power)
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
public class CurseCard : NoUseCard
{
    // ���� ī�忡 ���� Ưȭ�� �Ӽ���
    public int curseValue; // ���� ȿ�� ��

    // ���� ī�� ������
    public CurseCard(string name, string desc, int cost, int curseValue)
        : base(name, desc, cost, CardType.Curse)
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

//Ÿ��ī��
public class StrikeCard : AttackCard
{
    // Strike ī�� ������
    public StrikeCard(string name, string desc, int cost, int damage)
        : base(name, desc, cost, damage)
    {
        
    }

    // Strike ī���� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("Strike�� ����Ͽ� " + damage + "�� ���ظ� �����ϴ�!");
        // ���⼭ �߰����� ȿ���� ������ �� �ֽ��ϴ�.
    }
}
//����ī��
public class DefendCard : SkillCard
{
    // Defend ī�� ������
    public DefendCard(string name, string desc, int cost, int defenseAmount)
        : base(name, desc, cost, defenseAmount)
    {
    }
    // Defend ī���� Play �޼��带 �������մϴ�.
    public override void Play()
    {
        base.Play(); // �θ� Ŭ������ Play �޼��� ȣ��
        Debug.Log("Defend�� ����Ͽ� ������ " + effectValue + "��ŭ ������ŵ�ϴ�!");
        PlayerSc.instance.GainBlock(effectValue); // �÷��̾��� ������ ������ ����ŭ ������ŵ�ϴ�.
    }
}

